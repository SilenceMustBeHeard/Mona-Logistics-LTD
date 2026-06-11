// Page navigation with highway effect
document.addEventListener('DOMContentLoaded', function() {
    // Add road line to body
    const roadLine = document.createElement('div');
    roadLine.className = 'road-line';
    document.body.appendChild(roadLine);
    
    // Page transition on link clicks
    const links = document.querySelectorAll('a:not([target="_blank"]):not([href^="#"])');
    
    links.forEach(link => {
        // Skip external links and special cases
        if (link.hostname !== window.location.hostname) return;
        if (link.href.includes('javascript:')) return;
        
        link.addEventListener('click', function(e) {
            const targetUrl = this.href;
            if (!targetUrl || targetUrl === window.location.href) return;
            
            e.preventDefault();
            
            // Add exit animation
            document.body.classList.add('page-transition');
            
            // Play turn sound effect (optional - just visual)
            simulateTurnEffect();
            
            setTimeout(() => {
                window.location.href = targetUrl;
            }, 400);
        });
    });
    
    // Remove transition class on page load
    window.addEventListener('pageshow', function() {
        document.body.classList.remove('page-transition');
    });
    
    // Mobile menu toggle
    const menuToggle = document.querySelector('.menu-toggle');
    const navMenu = document.querySelector('.nav-menu');
    
    if (menuToggle && navMenu) {
        menuToggle.addEventListener('click', function() {
            navMenu.classList.toggle('active');
            animateMenuIcon(this);
        });
    }
    
    // Active link highlighting based on current route
    highlightActiveLink();
    
    // Smooth scroll for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
    
    // Add hover effect on cards with road turn effect
    const cards = document.querySelectorAll('.card, .stat-card');
    cards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-8px) translateX(4px)';
        });
        card.addEventListener('mouseleave', function() {
            this.style.transform = '';
        });
    });
});

// Simulate turn effect (visual feedback)
function simulateTurnEffect() {
    const turnEffect = document.createElement('div');
    turnEffect.style.position = 'fixed';
    turnEffect.style.top = '0';
    turnEffect.style.left = '0';
    turnEffect.style.width = '100%';
    turnEffect.style.height = '100%';
    turnEffect.style.background = 'linear-gradient(90deg, transparent, rgba(212, 160, 23, 0.1), transparent)';
    turnEffect.style.pointerEvents = 'none';
    turnEffect.style.zIndex = '9999';
    turnEffect.style.animation = 'shine 0.4s ease-out';
    document.body.appendChild(turnEffect);
    
    setTimeout(() => {
        turnEffect.remove();
    }, 400);
}

// Highlight active navigation link based on current URL
function highlightActiveLink() {
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        const linkPath = link.getAttribute('href');
        if (linkPath && linkPath !== '/' && currentPath.includes(linkPath)) {
            link.classList.add('active');
        } else if (linkPath === '/' && currentPath === '/') {
            link.classList.add('active');
        }
    });
}

// Animate mobile menu icon (hamburger to X)
function animateMenuIcon(button) {
    const spans = button.querySelectorAll('span');
    if (button.classList.contains('active')) {
        // Reset to hamburger
        spans.forEach(span => span.style.transform = '');
        button.classList.remove('active');
    } else {
        // Change to X
        if (spans.length >= 3) {
            spans[0].style.transform = 'rotate(45deg) translate(5px, 5px)';
            spans[1].style.opacity = '0';
            spans[2].style.transform = 'rotate(-45deg) translate(7px, -6px)';
        }
        button.classList.add('active');
    }
}

// Toast notifications with slide effect
function showToast(message, type = 'success') {
    const toast = document.createElement('div');
    toast.className = `toast-notification toast-${type}`;
    toast.innerHTML = `
        <div class="toast-icon">${type === 'success' ? '✅' : '❌'}</div>
        <div class="toast-message">${message}</div>
        <div class="toast-progress"></div>
    `;
    
    // Style the toast
    toast.style.position = 'fixed';
    toast.style.bottom = '20px';
    toast.style.right = '20px';
    toast.style.background = 'white';
    toast.style.borderRadius = '12px';
    toast.style.padding = '16px 20px';
    toast.style.boxShadow = '0 8px 24px rgba(0,0,0,0.15)';
    toast.style.display = 'flex';
    toast.style.alignItems = 'center';
    toast.style.gap = '12px';
    toast.style.zIndex = '10000';
    toast.style.animation = 'slideUp 0.3s ease';
    toast.style.borderLeft = `4px solid ${type === 'success' ? '#2a9d8f' : '#e63946'}`;
    
    document.body.appendChild(toast);
    
    setTimeout(() => {
        toast.style.animation = 'slideDown 0.3s ease';
        setTimeout(() => toast.remove(), 300);
    }, 4000);
}

// Add CSS for toast animation
const style = document.createElement('style');
style.textContent = `
    @keyframes slideDown {
        from { transform: translateY(0); opacity: 1; }
        to { transform: translateY(100px); opacity: 0; }
    }
`;
document.head.appendChild(style);

// Export for global use
window.showToast = showToast;