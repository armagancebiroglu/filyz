// ===============================
// Chat State (localStorage - taraf kapansa/taraycı kapansa da kalıcı)
// ===============================

const CHAT_STORAGE_KEY = "chatState";

const chatState = {
    activeTab: null,
    tabs: [],       // [{ nick, id, image }]
    messages: {}     // { [nick]: [{ name, message, image, id, time, isMe }] }
};

const MAX_MESSAGES_PER_TAB = 300; // depolama şişmesin diye üst sınır

// ── Kaydet / Yükle ────────────────────────────────────────────────
function saveChatState() {
    try {
        localStorage.setItem(CHAT_STORAGE_KEY, JSON.stringify(chatState));
    } catch (e) {
        console.error("chatState kaydedilemedi.", e);
    }
}

function loadChatState() {
    var saved = localStorage.getItem(CHAT_STORAGE_KEY);
    if (!saved) return;
    try {
        var state = JSON.parse(saved);
        chatState.activeTab = state.activeTab || null;
        chatState.tabs = state.tabs || [];
        chatState.messages = state.messages || {};
    } catch (e) {
        console.error("chatState okunamadı.", e);
    }
}

// Login sırasında farklı bir üye için tarayıcı paylaşılıyorsa geçmişin
// karışmaması adına çıkışta temizliyoruz (menüdeki "Çıkış" linkine bağlanıyor)
function clearChatState() {
    localStorage.removeItem(CHAT_STORAGE_KEY);
}

// ── Durumu güncelleyen yardımcılar ───────────────────────────────
function ensureTab(nickName, nickId, nickImg) {
    var exists = chatState.tabs.find(function (t) { return t.nick === nickName; });
    if (!exists) {
        chatState.tabs.push({ nick: nickName, id: nickId, image: nickImg, unread: false });
    } else {
        // id/resim güncel değilse tazele
        exists.id = nickId || exists.id;
        exists.image = nickImg || exists.image;
    }
}

function addMessageToState(nickName, msgObj) {
    if (!chatState.messages[nickName]) chatState.messages[nickName] = [];
    chatState.messages[nickName].push(msgObj);
    if (chatState.messages[nickName].length > MAX_MESSAGES_PER_TAB) {
        chatState.messages[nickName] = chatState.messages[nickName].slice(-MAX_MESSAGES_PER_TAB);
    }
}

function markUnread(nickName, isUnread) {
    var tab = chatState.tabs.find(function (t) { return t.nick === nickName; });
    if (tab) tab.unread = isUnread;
}

function saveOpenTab(nickName, nickId, nickImg) {
    ensureTab(nickName, nickId, nickImg);
    chatState.activeTab = nickName;
    saveChatState();
}

// ===============================
// DOM render fonksiyonları
// ===============================

function renderMessageHtml(msg) {
    if (msg.isMe) {
        return `<div class="message sent">
                    <img src="${msg.image}" class="Cmessage-avatar">
                    <div class="message-content">
                        <div class="message-bubble">${msg.message}</div>
                        <div class="Cmessage-time">${msg.time}</div>
                    </div>
                </div>`;
    }
    return `<div class="message received">
                <img src="${msg.image}" class="Cmessage-avatar">
                <div class="message-content">
                    <div class="message-sender">${msg.name}</div>
                    <div class="message-bubble">${msg.message}</div>
                    <div class="Cmessage-time">${msg.time}</div>
                </div>
            </div>`;
}

function buildTabContent(nickName) {
    var msgs = chatState.messages[nickName] || [];
    var $tab = $("<div>").addClass("tab-content").attr("id", nickName).hide();

    if (msgs.length === 0) {
        $tab.html(`<div class="chat-empty">
                        <div class="empty-icon"><i class="fa fa-comments"></i></div>
                        <div class="empty-title">${nickName}</div>
                        <div class="empty-title">Henüz mesaj yok</div>
                        <div class="empty-text">İlk mesajı göndererek sohbete başlayın!</div>
                    </div>`);
    } else {
        msgs.forEach(function (msg) {
            $tab.append(renderMessageHtml(msg));
        });
    }
    return $tab;
}

// Sayfa açılışında localStorage'daki tüm tab'ları ve mesajları DOM'a bas
function renderChatState() {
    // NOT: #contactsStrip burada temizlenmiyor — sunucunun (cshtml) baştan
    // bastığı favori üye chip'leri korunuyor. addChatListDom zaten aynı
    // nick için tekrar chip eklemiyor, o yüzden çakışma olmaz.
    $("#messagesArea .tab-content").remove();

    chatState.tabs.forEach(function (tab) {
        addChatListDom(tab.nick, tab.image, tab.id, tab.unread);
        $("#messagesArea").append(buildTabContent(tab.nick));
    });

    if (chatState.activeTab && chatState.tabs.some(function (t) { return t.nick === chatState.activeTab; })) {
        var activeTabData = chatState.tabs.find(function (t) { return t.nick === chatState.activeTab; });
        switchChatTab(activeTabData.nick, activeTabData.image, activeTabData.id, /*fromRender*/ true);
    }
}

function switchChatTab(NickName, NickImg, NickId, fromRender) {
    if (!fromRender) saveOpenTab(NickName, NickId, NickImg);

    $(".tab-content").hide();
    var $tab = $("#" + NickName);
    if ($tab.length === 0) {
        $tab = buildTabContent(NickName);
        $tab.appendTo("#messagesArea");
    }
    $tab.fadeIn();

    var Chat_header = `<a href="/Profil/Profil?member_id=${NickId}" target="_blank">
                    <img src="${NickImg}" class="chat-header-avatar" id="chatHeaderAvatar" alt="${NickName}">
                </a>
                <div class="chat-header-info">
                    <div class="chat-header-name" id="chatHeaderName">${NickName}</div>
                </div>`;
    $("#ChatHeader").empty().append(Chat_header);

    $("#mbrID").val(NickId);
    $("#nickNM").val(NickName);
    $("#messageInput").focus();

    // aktif tab'a geçilince: chip'te Cactive kalır, has-new kaldırılır
    markUnread(NickName, false);
    addChatListDom(NickName, NickImg, NickId, false);
    saveChatState();
}

// Sadece DOM'a chip ekler/günceller, state'e dokunmaz (state ayrı fonksiyonlarla yönetiliyor)
// - Chip yoksa oluşturur (favori olmayan, sonradan eklenen kişiler için)
// - Chip zaten varsa (favori üye chip'i dahil) sohbet tabı var demektir → Cactive eklenir
// - hasUnread true ise has-new eklenir, false ise kaldırılır
function addChatListDom(NickName, NickImg, NickId, hasUnread) {
    var currentUser = $(".contact-chip[data-tab='" + NickName + "']");
    if (currentUser.length === 0) {
        var inner_tab_content = `
                        <div class="chip-avatar-wrap">
                            <img src="${NickImg}" alt="${NickName}" class="chip-avatar">
                            <div class="status-dot Conline"></div>
                        </div>
                        <div class="chip-label">${NickName}</div>`;
        currentUser = $("<div>")
            .addClass("contact-chip")
            .attr("data-target", NickId)
            .attr("data-tab", NickName)
            .html(inner_tab_content)
            .appendTo("#contactsStrip");
    }

    currentUser.addClass("Cactive");
    currentUser.toggleClass("has-new", !!hasUnread);
}

// Dışarıdan (yeni kişi eklerken) çağrılan versiyon - hem state hem DOM
function addChatList(NickName, NickImg, NickId) {
    ensureTab(NickName, NickId, NickImg);
    addChatListDom(NickName, NickImg, NickId, false);
    saveChatState();
}

function chatFormSend() {
    const NickName = $("#nickNM").val();
    const NickImg = $("#hdnIMG").val();
    const NickId = $("#mbrID").val();
    const NickSex = $("#sex").val();
    if (NickName) {
        switchChatTab(NickName, NickImg, NickId);
        addChatList(NickName, NickImg, NickId);
    }  

} 

$(document).ready(function () {

    var isChatPage = ($("#messagesArea").length > 0);

    // Her sayfada state'i belleğe al (SignalR mesajlarını state'e yazabilmek için)
    loadChatState();

    // ══════════════════════════════════════════════════════════════
    // CHAT SAYFASINDAYSA: kayıtlı state'ten tüm tab/mesajları geri kur
    // ══════════════════════════════════════════════════════════════
    if (isChatPage) {
        renderChatState();

        // URL parametrelerini işle (?chat=X&img=Y&mid=Z) — bildirimden gelme
        const urlParams = new URLSearchParams(window.location.search);
        const targetChat = urlParams.get('chat');
        const targetImg = urlParams.get('img');
        const targetId = urlParams.get('mid');
        if (targetChat && targetImg) {
            addChatList(targetChat, targetImg, targetId);
            switchChatTab(targetChat, targetImg, targetId);
            window.history.replaceState({}, document.title, "/Home/Chat");
        }
    }

    // ══════════════════════════════════════════════════════════════
    // SignalR
    // ══════════════════════════════════════════════════════════════
    var chat = $.connection.chatHub;
    var myNick = $("#nick").val();

    chat.client.addNewMessageToPage = function (name, message, forWho, image, id) {
        const isMe = (name === myNick);
        const kime = isMe ? forWho : name;

        // Benimle ilgisiz mesajı atla
        if (name !== myNick && forWho !== myNick) return;

        const time = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
        const msgObj = { name, message, image, id, time, isMe };

        // ── Her durumda state'e (localStorage) yaz ───────────────
        ensureTab(kime, id, image);
        addMessageToState(kime, msgObj);

        // ── Bana gelen mesaj bildirimleri (her sayfada) ──────────
        if (!isMe && forWho === myNick) {
            $("#chatIcon").addClass("chatIcon");
            const safeImg = encodeURIComponent(image);
            const chatUrl = `/Home/Chat?chat=${name}&img=${safeImg}&mid=${id}`;
            $("#chatIcon").attr("href", chatUrl);
            new Audio('/Content/sounds/notification.wav').play().catch(() => { });
            document.title = `(${name}) Yeni Mesaj!`;

            if (chatState.activeTab !== kime) {
                markUnread(kime, true);
            }
        }

        saveChatState();

        // ── Buradan itibaren yalnızca CHAT SAYFASINDA DOM güncellenir ───
        if (!isChatPage) return;

        var chatWindow = $("#" + kime);
        if (chatWindow.length === 0) {
            chatWindow = buildTabContent(kime);
            chatWindow.appendTo("#messagesArea");
        }
        chatWindow.find(".chat-empty").remove();
        chatWindow.append(renderMessageHtml(msgObj));

        // Scroll
        $("#messagesArea").stop().animate({ scrollTop: $("#messagesArea")[0].scrollHeight }, 200);

        // chip: sohbet tabı var → Cactive; karşıdan geldi ve tab görünür değilse → has-new
        var chipUnread = !isMe && !chatWindow.is(":visible");
        addChatListDom(kime, image, id, chipUnread);
    };

    // ── SignalR bağlantısını başlat ──────────────────────────────
    $.connection.hub.start().done(function () {
        if (isChatPage) {
            $('#sendmessage').click(function () {
                const msg = $('#messageInput').val();
                const activeTab = $(".tab-content:visible");
                const targetNick = activeTab.attr("id");
                const myImg = $('#MyImg').val();

                if (msg.trim() !== "" && targetNick) {
                    chat.server.send(myNick, msg, targetNick, myImg, $("#mbrID").val());
                    $('#messageInput').val('').focus();
                }
            });
        }
    });

    // ── Contact chip tıklama ─────────────────────────────────────
    if (isChatPage) {
        $(document).on("click", ".contact-chip", function () {
            const NickName = $(this).attr("data-tab");
            const NickImg = $(this).find("img").attr("src");
            const NickId = $(this).attr("data-target");
            switchChatTab(NickName, NickImg, NickId);
        });

        // Enter ile gönder
        $('#messageInput').on('keypress', function (e) {
            if (e.which === 13) {
                e.preventDefault();
                $('#sendmessage').click();
            }
        });
    }

    // ── chatIcon tıklama (her sayfada) ───────────────────────────
    $('#chatIcon').on('click', function (e) {
        e.preventDefault();
        $(this).removeClass('chatIcon');
        const chatUrl = $(this).attr('href') || '/Home/Chat';

        if (window._chatWindow && !window._chatWindow.closed) {
            window._chatWindow.location.href = chatUrl;
            window._chatWindow.focus();
        } else {
            window._chatWindow = window.open(chatUrl, 'ChatWindow');
        }
    });

    // ── Çıkış yapılınca sohbet geçmişini temizle ─────────────────
    // Master sayfadaki "Çıkış" linkine id="logoutLink" eklemen gerekiyor:
    // <a href="/?w=1" id="logoutLink" class="menu-item">...
    $(document).on('click', '#logoutLink', function () {
        clearChatState();
    });
});

// ── Arka plan slaytı ─────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', function () {
    const images = [
        '../../Media/img/1.jpg',
        '../../Media/img/2.jpg',
        '../../Media/img/3.jpg',
        '../../Media/img/4.jpg',
        '../../Media/img/5.jpg'
    ];
    let currentImage = 0;
    function nextBackground() {
        currentImage = (currentImage + 1) % images.length;
        const img = new Image();
        img.src = images[currentImage];
        img.onload = function () {
            document.body.style.backgroundImage = `url('${images[currentImage]}')`;
        };
    }
    setInterval(nextBackground, 5000);
});
