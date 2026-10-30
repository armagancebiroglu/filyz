

var camera, scene, renderer;
var container, mesh;

function screen180View(W_H, container_id, modalAlb, URL) {
  
    THREE.ImageUtils.crossOrigin = '';
       
        var isUserInteracting = false,
          onMouseDownMouseX = 0,
          lon = -(W_H * 15.87),         
          onMouseDownLon = 0,
          phi = 0,
          theta = 0;
       

        if (W_H == 0 || container_id == "" || modalAlb == "" || URL == "")
        {
            camera = null;
            scene = null;
            renderer = null;
            mesh = null;
            return;
        }
       
        init();
        animate();
       
        function init() {
           
            //var container, mesh;
            container = document.getElementById(container_id);
           
            camera = new THREE.PerspectiveCamera(53, $(document.getElementById(container_id)).width() / $(document.getElementById(container_id)).height(), 1, 1100);
            camera.target = new THREE.Vector3(0, 0, 0);
            scene = new THREE.Scene();


            var geometry = new THREE.CylinderGeometry(5, 5, 5, 64, 1, true, 0, W_H);           
            geometry.applyMatrix(new THREE.Matrix4().makeScale(-1, 1, 1));

            var material = new THREE.MeshBasicMaterial({
                
                map: THREE.ImageUtils.loadTexture(URL)
            });
            material.map.minFilter = THREE.LinearFilter;

            mesh = new THREE.Mesh(geometry, material);
            scene.add(mesh);
        
           

            renderer = new THREE.WebGLRenderer();
            renderer.setPixelRatio(window.devicePixelRatio);
            renderer.setSize($(document.getElementById(container_id)).width(), $(document.getElementById(container_id)).height());
            container.appendChild(renderer.domElement);



            document.addEventListener('mousedown', onDocumentMouseDown, false);
            document.addEventListener('mousemove', onDocumentMouseMove, false);
            document.addEventListener('mouseup', onDocumentMouseUp, false);

            document.addEventListener('touchstart', function (event) {              
                isUserInteracting = true;
                onPointerDownPointerX = event.changedTouches[0].clientX;
                onPointerDownLon = lon;
            }, false);

            document.addEventListener('touchmove', function (event) {              
                if (isUserInteracting === true && event.touches.length === 1) {
                    lon = (onPointerDownPointerX - event.changedTouches[0].clientX) * 0.1 + onPointerDownLon;
                }
            }, false);

            document.addEventListener('touchend', onDocumentMouseUp, false);

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
              
               
                var reader = new FileReader();
                reader.addEventListener('load', function (event) {
                    material.map.image.src = event.target.result;
                    material.map.needsUpdate = true;
                }, false);
                reader.readAsDataURL(event.dataTransfer.files[0]);
                document.body.style.opacity = 1;
            }, false);
            window.addEventListener('resize', onWindowResize, false);
        }

        function onWindowResize() {
            camera.aspect = ($(document.getElementById(modalAlb)).width() - 30) / $(document.getElementById(container_id)).height();
            camera.updateProjectionMatrix();
            renderer.setSize(($(document.getElementById(modalAlb)).width() - 30), $(document.getElementById(container_id)).height());
        }

        function onDocumentMouseDown(event) {
            isUserInteracting = true;
            onPointerDownPointerX = event.clientX;
            onPointerDownLon = lon;
        }

        function onDocumentMouseMove(event) {
            if (isUserInteracting === true) {
                lon = (onPointerDownPointerX - event.clientX) * 0.1 + onPointerDownLon;
            }
        }

        function onDocumentMouseUp(event) {
            isUserInteracting = false;
        }
       
        function animate()
        {
            requestAnimationFrame(animate);

            if (camera!=null)
            {
                update();
            }
            else
            {
                return;
            }
        }

        var tr = true;
        function update() {           
            if (isUserInteracting === false)
            {

                if (tr)
                {
                    lon += 0.07;
                    if (lon >= -((W_H * 15.87) - 8))
                    {
                        tr = false;
                    }
                }
                else
                {
                    lon -= 0.07;
                    if (lon <= -((W_H * 15.87) + 8)) {
                        tr = true;
                    }
                }
            }
            
                phi = THREE.Math.degToRad(-90);
                theta = THREE.Math.degToRad(lon);
                camera.target.x = 500 * Math.sin(phi) * Math.cos(theta);
                camera.target.y = 500 * Math.cos(phi);
                camera.target.z = 500 * Math.sin(phi) * Math.sin(theta);
                camera.lookAt(camera.target);
                renderer.render(scene, camera);
            
        }
    }