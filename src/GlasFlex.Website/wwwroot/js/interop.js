window.enterFullscreen = () => {
    const el = document.documentElement;

    if (el.requestFullscreen) {
        return el.requestFullscreen();
    }

    if (el.webkitRequestFullscreen) {
        return el.webkitRequestFullscreen();
    }
};
