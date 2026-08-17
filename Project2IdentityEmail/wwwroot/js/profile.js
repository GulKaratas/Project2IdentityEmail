(function () {
    'use strict';

    var page = document.querySelector('.profile-page');
    if (!page) {
        return;
    }

    var avatarInput = page.querySelector('[data-avatar-input]');
    var avatarPreview = page.querySelector('[data-avatar-preview]');

    if (avatarInput && avatarPreview) {
        avatarInput.addEventListener('change', function () {
            var file = avatarInput.files && avatarInput.files[0];
            if (!file) {
                return;
            }

            var objectUrl = URL.createObjectURL(file);
            avatarPreview.addEventListener('load', function () {
                URL.revokeObjectURL(objectUrl);
            }, { once: true });
            avatarPreview.src = objectUrl;
        });
    }

    page.querySelectorAll('[data-password-toggle]').forEach(function (button) {
        button.addEventListener('click', function () {
            var field = button.closest('.profile-input');
            var input = field.querySelector('input');
            var reveal = input.type === 'password';

            input.type = reveal ? 'text' : 'password';
            field.classList.toggle('is-revealed', reveal);
            button.setAttribute('aria-label', reveal ? 'Şifreyi gizle' : 'Şifreyi göster');
        });
    });

    page.querySelectorAll('[data-preference]').forEach(function (input) {
        var storageKey = 'profile.' + input.getAttribute('data-preference');

        try {
            input.checked = localStorage.getItem(storageKey) === 'true';
            input.addEventListener('change', function () {
                localStorage.setItem(storageKey, String(input.checked));
            });
        } catch (error) {
            // Depolama kapalıysa anahtarlar oturum boyunca çalışmaya devam eder.
        }
    });
})();
