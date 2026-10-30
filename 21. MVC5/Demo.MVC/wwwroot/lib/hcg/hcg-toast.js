/*!
 * hcg-toast v1.0.0
 * Lightweight vanilla JavaScript toast / notification library. Zero dependencies.
 * Requires hcg-toast.css for the default styling.
 * When changing this file, also update README.md and website-page.html.
 * @see https://www.html-code-generator.com/javascript/toast-notification
 * @license MIT
 */
(function (root, factory) {
    const api = factory();
    if (typeof module === 'object' && module.exports) {
        module.exports = api;
    } else {
        root.toast = api;
    }
}(
    typeof globalThis !== 'undefined' ? globalThis : typeof window !== 'undefined' ? window : this,
    () => {

        const DEFAULT_DURATION = 4000;
        const DEFAULT_MAX_TOASTS = 5;
        const DEFAULT_POSITION = 'top-right';
        const EXIT_MS = 250;

        const POSITIONS = [
            'top-left', 'top-center', 'top-right',
            'bottom-left', 'bottom-center', 'bottom-right',
        ];

        const TYPE_ICONS = {
            info: 'ℹ',
            success: '✓',
            warning: '⚠',
            error: '✕',
        };

        const positionClass = (pos) => `hcg-toast-container--${pos}`;

        const normalizePosition = (value) => (
            POSITIONS.includes(value) ? value : DEFAULT_POSITION
        );

        const mergeOptions = (defaults, overrides) => {
            const merged = { ...defaults, ...overrides };
            merged.position = normalizePosition(merged.position);
            return merged;
        };

        const applyA11y = (el, options) => {
            const live = options.ariaLive === 'polite' || options.ariaLive === 'assertive' || options.ariaLive === 'off'
                ? options.ariaLive
                : (options.type === 'error' ? 'assertive' : 'off');
            el.setAttribute('role', options.type === 'error' ? 'alert' : 'status');
            el.setAttribute('aria-live', live);
        };

        const createToastInstance = (initialOptions = {}) => {
            let _toastId = 0;
            let destroyed = false;
            let container = null;
            const toasts = new Map();

            let defaults = mergeOptions({
                type: 'info',
                duration: DEFAULT_DURATION,
                closable: true,
                pauseOnHover: true,
                position: DEFAULT_POSITION,
                maxToasts: DEFAULT_MAX_TOASTS,
                showProgress: true,
            }, initialOptions);

            let containerListenersAttached = false;

            const getToastEntry = (host, e) => {
                const item = e.target.closest('.hcg-toast-item');
                if (!item || !host.contains(item) || item.contains(e.relatedTarget)) return null;
                return toasts.get(Number(item.dataset.toastId)) || null;
            };

            const attachContainerListeners = (host) => {
                if (containerListenersAttached) return;
                containerListenersAttached = true;

                host.addEventListener('click', (e) => {
                    if (!e.target.closest('.hcg-toast-close')) return;
                    const item = e.target.closest('.hcg-toast-item');
                    if (!item) return;
                    removeEntry(Number(item.dataset.toastId), 'close');
                });

                host.addEventListener('mouseover', (e) => {
                    const entry = getToastEntry(host, e);
                    if (entry && entry.options.pauseOnHover && entry.options.duration > 0) {
                        pauseTimer(entry);
                    }
                });

                host.addEventListener('mouseout', (e) => {
                    const entry = getToastEntry(host, e);
                    if (entry && entry.options.pauseOnHover && entry.options.duration > 0) {
                        resumeTimer(entry);
                    }
                });
            };

            const cancelEntryFrame = (entry) => {
                if (entry.rafId != null) {
                    cancelAnimationFrame(entry.rafId);
                    entry.rafId = null;
                }
            };

            const ensureContainer = () => {
                if (destroyed) return null;
                if (container && document.body.contains(container)) {
                    return container;
                }
                container = document.createElement('div');
                container.className = `hcg-toast-container ${positionClass(defaults.position)}`;
                document.body.appendChild(container);
                containerListenersAttached = false;
                attachContainerListeners(container);
                return container;
            };

            const updateContainerPosition = () => {
                if (!container) return;
                container.className = `hcg-toast-container ${positionClass(defaults.position)}`;
            };

            const clearTimer = (entry) => {
                if (entry.timerId != null) {
                    clearTimeout(entry.timerId);
                    entry.timerId = null;
                }
            };

            const removeEntry = (id, reason, immediate = false) => {
                const entry = toasts.get(id);
                if (!entry || entry.removing) return;
                entry.removing = true;
                clearTimer(entry);

                const { el, options } = entry;

                const finish = () => {
                    cancelEntryFrame(entry);
                    if (el.parentNode) el.parentNode.removeChild(el);
                    toasts.delete(id);
                    if (typeof options.onDismiss === 'function') {
                        options.onDismiss({ id, ...options }, reason);
                    }
                };

                if (immediate) {
                    finish();
                    return;
                }

                el.classList.remove('hcg-toast-item--enter');
                el.classList.add('hcg-toast-item--exit');

                let done = false;
                const onEnd = (e) => {
                    if (e.target !== el || done) return;
                    done = true;
                    el.removeEventListener('transitionend', onEnd);
                    finish();
                };

                el.addEventListener('transitionend', onEnd);
                setTimeout(() => {
                    if (!done) {
                        done = true;
                        el.removeEventListener('transitionend', onEnd);
                        finish();
                    }
                }, EXIT_MS + 50);
            };

            const startProgressAnimation = (entry) => {
                if (!entry.progressEl) return;
                const ms = entry.remaining != null ? entry.remaining : entry.options.duration;
                const bar = entry.progressEl;
                bar.style.animation = 'none';
                void bar.offsetWidth;
                bar.style.animation = `hcg-toast-progress-shrink ${ms}ms linear forwards`;
            };

            const scheduleDismiss = (entry) => {
                clearTimer(entry);
                if (entry.options.duration <= 0 || entry.paused) return;

                const remaining = entry.remaining != null
                    ? entry.remaining
                    : entry.options.duration;

                entry.remaining = remaining;
                entry.timerStarted = Date.now();

                entry.timerId = setTimeout(() => {
                    removeEntry(entry.id, 'timeout');
                }, remaining);

                startProgressAnimation(entry);
            };

            const pauseTimer = (entry) => {
                if (entry.paused || entry.options.duration <= 0) return;
                if (entry.timerStarted == null) return;
                entry.paused = true;
                clearTimer(entry);
                const elapsed = Date.now() - entry.timerStarted;
                entry.remaining = Math.max(0, (entry.remaining ?? entry.options.duration) - elapsed);
                if (entry.progressEl) {
                    entry.progressEl.style.animationPlayState = 'paused';
                }
            };

            const resumeTimer = (entry) => {
                if (!entry.paused || entry.options.duration <= 0) return;
                entry.paused = false;
                scheduleDismiss(entry);
            };

            const enforceMaxToasts = (maxToasts) => {
                while (toasts.size >= maxToasts) {
                    const oldest = toasts.keys().next().value;
                    if (oldest == null) break;
                    removeEntry(oldest, 'api', true);
                }
            };

            const buildToastEl = (options, id) => {
                const el = document.createElement('div');
                el.className = `hcg-toast-item hcg-toast-item--${options.type}`;
                applyA11y(el, options);
                el.dataset.toastId = String(id);

                if (options.icon !== false) {
                    const iconEl = document.createElement('span');
                    iconEl.className = 'hcg-toast-icon';
                    iconEl.setAttribute('aria-hidden', 'true');

                    if (options.iconHtml) {
                        iconEl.innerHTML = options.iconHtml;
                    } else if (typeof options.icon === 'string') {
                        iconEl.textContent = options.icon;
                    } else {
                        iconEl.textContent = TYPE_ICONS[options.type] || TYPE_ICONS.info;
                    }

                    el.appendChild(iconEl);
                }

                const body = document.createElement('div');
                body.className = 'hcg-toast-body';

                if (options.title) {
                    const titleEl = document.createElement('div');
                    titleEl.className = 'hcg-toast-title';
                    titleEl.textContent = options.title;
                    body.appendChild(titleEl);
                }

                const messageEl = document.createElement('div');
                messageEl.className = 'hcg-toast-message';
                messageEl.textContent = options.message;
                body.appendChild(messageEl);

                el.appendChild(body);

                if (options.closable) {
                    const closeBtn = document.createElement('button');
                    closeBtn.type = 'button';
                    closeBtn.className = 'hcg-toast-close';
                    closeBtn.setAttribute('aria-label', 'Dismiss notification');
                    closeBtn.innerHTML = '&times;';
                    el.appendChild(closeBtn);
                }

                let progressEl = null;
                if (options.duration > 0 && options.showProgress) {
                    const trackEl = document.createElement('div');
                    trackEl.className = 'hcg-toast-progress-track';

                    progressEl = document.createElement('div');
                    progressEl.className = 'hcg-toast-progress';

                    trackEl.appendChild(progressEl);
                    el.appendChild(trackEl);
                }

                return { el, progressEl };
            };

            const show = (message, options = {}) => {
                if (destroyed) return null;

                const merged = mergeOptions(defaults, {
                    ...options,
                    message: message != null ? String(message) : options.message,
                });

                if (!merged.message) return null;

                const host = ensureContainer();
                if (!host) return null;

                const id = ++_toastId;
                const { el, progressEl } = buildToastEl(merged, id);

                const entry = {
                    id,
                    el,
                    progressEl,
                    options: merged,
                    paused: false,
                    remaining: merged.duration,
                    removing: false,
                    timerId: null,
                    timerStarted: null,
                    rafId: null,
                };

                enforceMaxToasts(merged.maxToasts);

                toasts.set(id, entry);
                host.appendChild(el);

                entry.rafId = requestAnimationFrame(() => {
                    entry.rafId = null;
                    if (!toasts.has(id)) return;
                    el.classList.add('hcg-toast-item--enter');
                    if (merged.duration > 0) {
                        scheduleDismiss(entry);
                    }
                    if (typeof merged.onShow === 'function') {
                        merged.onShow({ id, ...merged });
                    }
                });

                return id;
            };

            const typedShow = (type) => (message, options = {}) => (
                show(message, { ...options, type })
            );

            const dismiss = (id) => {
                if (id == null) return;
                removeEntry(id, 'api');
            };

            const dismissAll = () => {
                [...toasts.keys()].forEach((id) => removeEntry(id, 'api', true));
            };

            const configure = (partialOptions = {}) => {
                if (destroyed) return;
                defaults = mergeOptions(defaults, partialOptions);
                updateContainerPosition();
            };

            const destroy = () => {
                if (destroyed) return;
                dismissAll();
                if (container && container.parentNode) {
                    container.parentNode.removeChild(container);
                }
                container = null;
                containerListenersAttached = false;
                destroyed = true;
            };

            return {
                show,
                success: typedShow('success'),
                error: typedShow('error'),
                warning: typedShow('warning'),
                info: typedShow('info'),
                dismiss,
                dismissAll,
                configure,
                destroy,
            };
        };

        let defaultInstance = null;

        const getDefaultInstance = () => {
            if (!defaultInstance) {
                defaultInstance = createToastInstance();
            }
            return defaultInstance;
        };

        const toastFn = (message, options) => getDefaultInstance().show(message, options);

        toastFn.success = (message, options) => getDefaultInstance().success(message, options);
        toastFn.error = (message, options) => getDefaultInstance().error(message, options);
        toastFn.warning = (message, options) => getDefaultInstance().warning(message, options);
        toastFn.info = (message, options) => getDefaultInstance().info(message, options);
        toastFn.dismiss = (id) => getDefaultInstance().dismiss(id);
        toastFn.dismissAll = () => getDefaultInstance().dismissAll();
        toastFn.configure = (options) => getDefaultInstance().configure(options);
        toastFn.destroy = () => {
            if (defaultInstance) {
                defaultInstance.destroy();
                defaultInstance = null;
            }
        };
        toastFn.create = (options) => createToastInstance(options);

        return toastFn;
    }
));
