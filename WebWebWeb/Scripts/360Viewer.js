

var camera2, scene2, renderer2;
var container2, mesh2;

function screen360View(container_id, modalAlb, URL) {

    THREE.ImageUtils.crossOrigin = '';
   

    var isUserInteracting2 = false,
        onMouseDownMouseX = 0, onMouseDownMouseY = 0,
        lon = 0, onMouseDownLon = 0,
        lat = 0, onMouseDownLat = 0,
        phi = 0, theta = 0;


    if (container_id == "" || modalAlb == "" || URL=="") {
       
        camera2 = null;
        scene2 = null;
        renderer2 = null;
        mesh2 = null;
        return;
    }
   
    init();
    animate();
    function init() {

        //var container, mesh;
        container2 = document.getElementById(container_id);
        camera2 = new THREE.PerspectiveCamera(53, $(document.getElementById(container_id)).width() / $(document.getElementById(container_id)).height(), 1, 1100);
        camera2.target = new THREE.Vector3(0, 0, 0);
        scene2 = new THREE.Scene();

        var geometry2 = new THREE.SphereGeometry(500, 60, 40);
        geometry2.applyMatrix(new THREE.Matrix4().makeScale(-1, 1, 1));

        var material2 = new THREE.MeshBasicMaterial({
            map: THREE.ImageUtils.loadTexture(URL)
        });

        mesh2 = new THREE.Mesh(geometry2, material2);

        scene2.add(mesh2);

        renderer2 = new THREE.WebGLRenderer();
        renderer2.setPixelRatio(window.devicePixelRatio);
        renderer2.setSize($(document.getElementById(container_id)).width(), $(document.getElementById(container_id)).height());
        container2.appendChild(renderer2.domElement);
        if (camera2 != null)//camera2.target
        {
            document.addEventListener('mousedown', onDocumentMouseDown, false);
            document.addEventListener('mousemove', onDocumentMouseMove, false);
            document.addEventListener('mouseup', onDocumentMouseUp, false);
            //document.addEventListener('mousewheel', onDocumentMouseWheel, false);
            //document.addEventListener('DOMMouseScroll', onDocumentMouseWheel, false);
            //
            document.addEventListener('touchstart', onDocumentTouchstart, false);
            document.addEventListener('touchend', onDocumentTouchend, false);
            //
            function onDocumentTouchstart(event) {

                isUserInteracting = true;
                onPointerDownPointerX = event.touches[0].clientX;
                onPointerDownPointerY = event.touches[0].clientY;
                onPointerDownLon = lon;
                onPointerDownLat = lat;
            }
            function onDocumentTouchend(event) {
                isUserInteracting = false;
            }
            document.addEventListener("touchmove", function (event) {
                if (isUserInteracting === true && event.touches.length === 1) {                    
                    lon = (onPointerDownPointerX - event.changedTouches[0].clientX) * 0.1 + onPointerDownLon;
                    lat = (event.changedTouches[0].clientY - onPointerDownPointerY) * 0.1 + onPointerDownLat;
                }
            }, false);            

            document.addEventListener('dragover', function (event) {
                event.dataTransfer.dropEffect = 'copy';
            }, false);

            document.addEventListener('dragenter', function (event) {
                document.body.style.opacity = 0.5;
            }, false);

            document.addEventListener('dragleave', function (event) {
                document.body.style.opacity = 1;
            }, false);

            document.addEventListener('drop', function (event) {

                var reader2 = new FileReader();
                reader2.addEventListener('load', function (event) {

                    material2.map.image.src = event.target.result;
                    material2.map.needsUpdate = true;

                }, false);
                reader2.readAsDataURL(event.dataTransfer.files[0]);

                document.body.style.opacity = 1;

            }, false);

            //

            window.addEventListener('resize', onWindowResize, false);
        }
        else
        {
            return;
        }
    }

    function onWindowResize() {
        if (camera2 != null)
        {
            camera2.aspect = ($(document.getElementById(modalAlb)).width() - 30) / $(document.getElementById(container_id)).height();
            camera2.updateProjectionMatrix();
            renderer2.setSize(($(document.getElementById(modalAlb)).width() - 30), $(document.getElementById(container_id)).height());
        }
        else
        {
            return;
        }
    }

    function onDocumentMouseDown(event) {
               
        isUserInteracting2 = true;

        onPointerDownPointerX = event.clientX;
        onPointerDownPointerY = event.clientY;

        onPointerDownLon = lon;
        onPointerDownLat = lat;

    }

    function onDocumentMouseMove(event) {

        if (isUserInteracting2 === true) {

            lon = (onPointerDownPointerX - event.clientX) * 0.1 + onPointerDownLon;
            lat = (event.clientY - onPointerDownPointerY) * 0.1 + onPointerDownLat;

        }

    }

    function onDocumentMouseUp(event) {

        isUserInteracting2 = false;

    }

    function onDocumentMouseWheel(event)
    {
        if (camera2 != null)
        {
            // WebKit

            if (event.wheelDeltaY) {

                camera2.fov -= event.wheelDeltaY * 0.05;

                // Opera / Explorer 9

            } else if (event.wheelDelta) {

                camera2.fov -= event.wheelDelta * 0.05;

                // Firefox

            } else if (event.detail) {

                camera2.fov += event.detail * 1.0;

            }

            camera2.updateProjectionMatrix();
        } else {
            return;
        }
    }

    function animate() {

        requestAnimationFrame(animate);

        if (camera2 != null)//camera2.target
        {
            update();
        }
        else
        {
            return;
        }

    }

    function update() {

        if (isUserInteracting2 === false) {

            lon += 0.1;

        }

        lat = Math.max(-85, Math.min(85, lat));
        phi = THREE.Math.degToRad(90 - lat);
        theta = THREE.Math.degToRad(lon);

        camera2.target.x = 500 * Math.sin(phi) * Math.cos(theta);
        camera2.target.y = 500 * Math.cos(phi);
        camera2.target.z = 500 * Math.sin(phi) * Math.sin(theta);

        camera2.lookAt(camera2.target);

        /*
                      // distortion
                      camera.position.copy( camera.target ).negate();
                      */

        renderer2.render(scene2, camera2);

    }
}