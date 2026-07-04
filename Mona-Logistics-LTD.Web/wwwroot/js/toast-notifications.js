(function () {
    'use strict';

    // Toast configuration
    const config = {
        containerClass: 'toast-container',
        position: 'bottom-0 end-0',
        autoHideDelay: 5000,
        maxToasts: 5
    };

    // Color palette for different toast types (Logistics theme)
    const toastColors = {
        success: {
            bg: 'rgba(40, 167, 69, 0.12)',
            border: '#28a745',
            icon: 'bi-check-circle-fill',
            text: 'rgba(255,255,255,0.9)'
        },
        error: {
            bg: 'rgba(220, 53, 69, 0.12)',
            border: '#dc3545',
            icon: 'bi-exclamation-triangle-fill',
            text: 'rgba(255,255,255,0.9)'
        },
        warning: {
            bg: 'rgba(255, 193, 7, 0.12)',
            border: '#ffc107',
            icon: 'bi-exclamation-triangle-fill',
            text: 'rgba(255,255,255,0.9)'
        },
        info: {
            bg: 'rgba(23, 162, 184, 0.12)',
            border: '#17a2b8',
            icon: 'bi-info-circle-fill',
            text: 'rgba(255,255,255,0.9)'
        }
    };

    // Get localized messages
    function getLocalizedMessages() {
        const culture = document.documentElement.lang || 'bg';
        const isBulgarian = culture.startsWith('bg');

        return {
            successTitle: isBulgarian ? 'Успешно!' : 'Success!',
            errorTitle: isBulgarian ? 'Грешка!' : 'Error!',
            warningTitle: isBulgarian ? 'Внимание' : 'Warning',
            infoTitle: isBulgarian ? 'Информация' : 'Info'
        };
    }

    // Create or get toast container
    function getToastContainer() {
        let container = document.querySelector(`.${config.containerClass}`);
        if (!container) {
            container = document.createElement('div');
            container.className = `${config.containerClass} position-fixed ${config.position} p-3`;
            container.style.zIndex = '1100';
            document.body.appendChild(container);
        }
        return container;
    }

    // Create a toast element
    function createToastElement(title, message, type = 'success') {
        const colors = toastColors[type] || toastColors.success;
        const messages = getLocalizedMessages();

        // Title mapping
        const titleMap = {
            success: messages.successTitle,
            error: messages.errorTitle,
            warning: messages.warningTitle,
            info: messages.infoTitle
        };
        const finalTitle = title || titleMap[type] || messages.infoTitle;

        const toast = document.createElement('div');
        toast.className = `toast align-items-center show mb-2`;
        toast.setAttribute('role', 'alert');
        toast.style.cssText = `
            background: var(--logistics-primary-dark);
            border: 1px solid rgba(212, 160, 23, 0.15);
            border-left: 4px solid ${colors.border};
            border-radius: 12px;
            min-width: 300px;
            backdrop-filter: blur(10px);
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.3);
            opacity: 0;
            transform: translateX(100px);
            transition: all 0.4s cubic-bezier(0.68, -0.55, 0.265, 1.55);
            overflow: hidden;
            padding: 0;
        `;

        toast.innerHTML = `
            <div class="toast-header" style="
                background: rgba(212, 160, 23, 0.05);
                color: white;
                border-bottom: 1px solid rgba(212, 160, 23, 0.08);
                padding: 12px 16px;
                display: flex;
                align-items: center;
                justify-content: space-between;
            ">
                <div style="display: flex; align-items: center; gap: 10px;">
                    <i class="bi ${colors.icon}" style="color: ${colors.border}; font-size: 1.2rem;"></i>
                    <strong style="font-weight: 600; font-size: 0.95rem;">${finalTitle}</strong>
                </div>
                <button type="button" class="btn-close" data-bs-dismiss="toast" 
                        style="filter: invert(1); opacity: 0.6; font-size: 0.8rem;"
                        aria-label="Close"></button>
            </div>
            <div class="toast-body" style="
                padding: 14px 16px;
                color: rgba(255, 255, 255, 0.85);
                font-size: 0.9rem;
                display: flex;
                align-items: center;
                gap: 10px;
            ">
                <i class="bi bi-truck" style="color: var(--logistics-accent); font-size: 1rem;"></i>
                <span>${message}</span>
            </div>
            <div class="toast-progress" style="
                height: 2px;
                background: ${colors.border};
                width: 100%;
                animation: toastProgress ${config.autoHideDelay}ms linear forwards;
            "></div>
        `;

        // Add progress animation
        const style = document.createElement('style');
        style.textContent = `
            @keyframes toastProgress {
                from { width: 100%; }
                to { width: 0%; }
            }
        `;
        if (!document.querySelector('#toastStyles')) {
            style.id = 'toastStyles';
            document.head.appendChild(style);
        }

        return toast;
    }

    // Main showToast function
    function showToast(title, message, type = 'success') {
        if (!message) return;

        const container = getToastContainer();
        const toast = createToastElement(title, message, type);

        // Limit max toasts
        while (container.children.length >= config.maxToasts) {
            const firstChild = container.firstChild;
            if (firstChild) {
                firstChild.style.opacity = '0';
                firstChild.style.transform = 'translateX(100px)';
                setTimeout(() => firstChild.remove(), 300);
            }
        }

        container.appendChild(toast);

        // Trigger entrance animation
        requestAnimationFrame(() => {
            toast.style.opacity = '1';
            toast.style.transform = 'translateX(0)';
        });

        // Auto hide after delay
        const timer = setTimeout(() => {
            hideToast(toast);
        }, config.autoHideDelay);

        // Close button handler
        const closeBtn = toast.querySelector('.btn-close');
        if (closeBtn) {
            closeBtn.addEventListener('click', () => {
                clearTimeout(timer);
                hideToast(toast);
            });
        }

        return toast;
    }

    function hideToast(toast) {
        toast.style.opacity = '0';
        toast.style.transform = 'translateX(100px)';
        setTimeout(() => {
            if (toast.parentNode) {
                toast.remove();
            }
        }, 300);
    }

    // Read TempData from hidden inputs
    function readTempData() {
        const types = ['Success', 'Error', 'Warning', 'Info'];
        const typeMap = {
            'Success': 'success',
            'Error': 'error',
            'Warning': 'warning',
            'Info': 'info'
        };

        types.forEach(type => {
            const input = document.getElementById(`tempData${type}`);
            if (input) {
                const value = input.value;
                if (value && value !== '' && value !== 'null') {
                    showToast(null, value, typeMap[type]);
                    input.value = '';
                }
            }
        });
    }

    // Global AJAX listener for toast headers
    function setupAjaxListener() {
        if (typeof $ !== 'undefined') {
            $(document).ajaxComplete(function (event, xhr) {
                const toast = xhr.getResponseHeader('X-Toast-Message');
                const toastType = xhr.getResponseHeader('X-Toast-Type') || 'success';
                if (toast) {
                    showToast(null, toast, toastType);
                }
            });
        }
    }

    // Initialize
    document.addEventListener('DOMContentLoaded', function () {
        readTempData();
        setupAjaxListener();
    });

    // Expose to global scope
    window.showToast = showToast;

})();