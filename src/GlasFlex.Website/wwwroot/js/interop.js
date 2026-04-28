window.enterFullscreen = () => {
    const el = document.documentElement;

    if (el.requestFullscreen) {
        return el.requestFullscreen();
    }

    if (el.webkitRequestFullscreen) {
        return el.webkitRequestFullscreen();
    }
};

window.moveToBody = (id) => {
    const el = document.getElementById(id);

    if(!el || el.parentElement === document.body){
        return;
    }

    document.body.appendChild(el);
};