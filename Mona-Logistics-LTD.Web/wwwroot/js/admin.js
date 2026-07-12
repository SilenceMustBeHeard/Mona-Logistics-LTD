document.addEventListener('DOMContentLoaded', function () {
    // Stats animation with counter
    function animateNumber(element, target, duration = 2000) {
        const start = 0;
        const startTime = performance.now();

        function update(currentTime) {
            const elapsed = currentTime - startTime;
            const progress = Math.min(elapsed / duration, 1);
            const current = Math.floor(progress * target);
            element.textContent = current.toLocaleString();

            if (progress < 1) {
                requestAnimationFrame(update);
            } else {
                element.textContent = target.toLocaleString();
            }
        }

        requestAnimationFrame(update);
    }

    // Glow effect on cards
    document.querySelectorAll('.action-card').forEach(card => {
        card.addEventListener('mouseenter', function (e) {
            const rect = this.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;
            this.style.setProperty('--mouse-x', x + 'px');
            this.style.setProperty('--mouse-y', y + 'px');
        });
    });

    // Micro-interactions on stats
    document.querySelectorAll('.stat-card').forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-8px)';
            this.style.boxShadow = '0 12px 48px rgba(212, 160, 23, 0.15)';
        });
        card.addEventListener('mouseleave', function () {
            this.style.transform = '';
            this.style.boxShadow = '';
        });
    });

    // Update activity times
    function updateActivityTimes() {
        const items = document.querySelectorAll('.activity-item .activity-time');
        // In a real app, you'd fetch actual timestamps
    }
    updateActivityTimes();

    // Auto refresh stats every 60 seconds
    setInterval(() => {
        fetch('/api/admin/stats')
            .then(response => response.json())
            .then(data => {
                const stats = {
                    'loadRequestsCount': data.totalRequests,
                    'usersCount': data.totalUsers,
                    'trucksCount': data.activeTrucks,
                    'messagesCount': data.newMessages
                };
                Object.keys(stats).forEach(id => {
                    const el = document.getElementById(id);
                    if (el) {
                        const current = parseInt(el.textContent.replace(/,/g, ''));
                        if (!isNaN(current) && current !== stats[id]) {
                            animateNumber(el, stats[id] || 0, 1000);
                        }
                    }
                });
            })
            .catch(() => { });
    }, 60000);

    // Particle background effect
    const hero = document.querySelector('.dashboard-hero');
    if (hero) {
        for (let i = 0; i < 8; i++) {
            const particle = document.createElement('div');
            particle.className = 'hero-particle';
            particle.style.cssText = `
                position: absolute;
                width: ${Math.random() * 4 + 2}px;
                height: ${Math.random() * 4 + 2}px;
                background: rgba(212, 160, 23, ${Math.random() * 0.2 + 0.05});
                border-radius: 50%;
                left: ${Math.random() * 100}%;
                top: ${Math.random() * 100}%;
                animation: floatParticle ${Math.random() * 20 + 15}s linear infinite;
                animation-delay: ${Math.random() * 5}s;
                pointer-events: none;
            `;
            hero.appendChild(particle);
        }
    }

    // Add floating particle styles if not exists
    if (!document.querySelector('#particleStyles')) {
        const style = document.createElement('style');
        style.id = 'particleStyles';
        style.textContent = `
            @keyframes floatParticle {
                0% { transform: translateY(0) translateX(0) scale(1); opacity: 0; }
                25% { opacity: 1; }
                75% { opacity: 1; }
                100% { transform: translateY(-100px) translateX(30px) scale(0.5); opacity: 0; }
            }
        `;
        document.head.appendChild(style);
    }
});