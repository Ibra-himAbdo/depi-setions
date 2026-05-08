const AppToast = (function () {
    const defaults = {
        duration: 3500,
        close: true,
        gravity: "top",
        position: "right",
        stopOnFocus: true,
    };

    function getDirectionalPosition() {
        const dir = document.documentElement.getAttribute("dir");
        return dir === "rtl" ? "left" : "right";
    }

    function createToast(type, message, userOptions = {}) {
        const options = { ...defaults, ...userOptions };
        if (!Object.prototype.hasOwnProperty.call(userOptions, "position")) {
            options.position = getDirectionalPosition();
        }

        let iconHtml = "";
        let background = "";

        switch (type.toLowerCase()) {
            case "success":
                iconHtml = '<i class="fa-solid fa-circle-check fa-lg" style="margin-inline-end: 12px; color: rgba(255,255,255,0.9);"></i>';
                background = "linear-gradient(135deg, #10b981 0%, #059669 100%)";
                break;
            case "error":
                iconHtml = '<i class="fa-solid fa-circle-xmark fa-lg" style="margin-inline-end: 12px; color: rgba(255,255,255,0.9);"></i>';
                background = "linear-gradient(135deg, #ef4444 0%, #dc2626 100%)";
                options.duration = 6000;
                break;
            case "warning":
                iconHtml = '<i class="fa-solid fa-triangle-exclamation fa-lg" style="margin-inline-end: 12px; color: rgba(255,255,255,0.9);"></i>';
                background = "linear-gradient(135deg, #f59e0b 0%, #d97706 100%)";
                break;
            case "info":
            default:
                iconHtml = '<i class="fa-solid fa-circle-info fa-lg" style="margin-inline-end: 12px; color: rgba(255,255,255,0.9);"></i>';
                background = "linear-gradient(135deg, #3b82f6 0%, #2563eb 100%)";
                break;
        }
 
        Toastify({
            text: `<div style="display: flex; align-items: center; font-weight: 500; letter-spacing: 0.3px;">${iconHtml}<span style="word-break: break-word;">${message}</span></div>`,
            duration: options.duration,
            close: options.close,
            gravity: options.gravity,
            position: options.position,
            stopOnFocus: options.stopOnFocus,
            escapeMarkup: false,
            style: {
                background: background,
                color: "#fff",
                borderRadius: "14px",
                boxShadow: "0 20px 25px -5px rgba(0, 0, 0, 0.15), 0 10px 10px -5px rgba(0, 0, 0, 0.1)",
                fontFamily: "'Inter', system-ui, -apple-system, sans-serif",
                padding: "16px 24px",
                border: "1px solid rgba(255, 255, 255, 0.1)",
                maxWidth: "450px",
                lineHeight: "1.5",
                display: "flex",
                alignItems: "center",
                wordBreak: "break-word"
            }
        }).showToast();
    }

    return {
        success: (msg, options) => createToast("success", msg, options),
        error: (msg, options) => createToast("error", msg, options),
        warning: (msg, options) => createToast("warning", msg, options),
        info: (msg, options) => createToast("info", msg, options),

        raw: (opts) => Toastify(opts).showToast()
    };
})();

window.AppToast = AppToast;

document.addEventListener("DOMContentLoaded", function () {
    const toastData = document.getElementById("server-toast");

    if (toastData) {
        const type = toastData.getAttribute("data-type");
        const message = toastData.getAttribute("data-message");

        if (type && message) {
            if (type === "success") AppToast.success(message);
            else if (type === "error") AppToast.error(message);
            else if (type === "warning") AppToast.warning(message);
            else AppToast.info(message);
        }
    }
});