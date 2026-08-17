const features = [
    ['category', 'Akıllı Kategoriler', 'Gelen mesajlarınızı iş, arkadaş veya spam gibi kategorilere otomatik ayırın.'],
    ['lock', 'Güvenli Giriş (2FA)', 'E-postanıza gönderilen 6 haneli kod ile iki adımlı doğrulama.'],
    ['verified_user', 'E-posta Doğrulama', 'Hesabınızı aktifleştirmek için gönderilen kod ile sahte kayıtları önleyin.'],
    ['edit_document', 'Zengin Metin Editörü', 'Mesajlarınızı biçimlendirerek yazın, tam olarak yazdığınız şekilde iletilsin.'],
    ['inbox', 'Gelen Kutusu Yönetimi', 'Mesajları görüntüleyin, detaylarını inceleyin ve tek tıkla silin.'],
    ['query_stats', 'Mesaj İstatistikleri', 'Gönderilen ve alınan mesajlarınızı grafiklerle takip edin.']
];

const stats = [
    ['100', '%', 'Güvenli'],
    ['99.9', '%', 'Çalışma Süresi'],
    ['50', 'M+', 'İşlenen E-posta'],
    ['24', '/7', 'Erişilebilir']
];

// Features
features.forEach(([icon, title, desc]) => {
    const card = document.createElement('div');
    card.className = 'card bg-white/5 border border-white/10 rounded-2xl p-6 cursor-pointer transition-all duration-300';

    // Hover efektleri JS ile
    card.addEventListener('mouseenter', () => {
        card.style.transform = 'translateY(-8px) scale(1.02)';
        card.style.boxShadow = '0 25px 50px rgba(192,193,255,0.15)';
        card.style.borderColor = 'rgba(192,193,255,0.3)';
        iconEl.style.background = 'rgba(192,193,255,0.2)';
        iconEl.style.color = '#c0c1ff';
    });
    card.addEventListener('mouseleave', () => {
        card.style.transform = '';
        card.style.boxShadow = '';
        card.style.borderColor = '';
        iconEl.style.background = '';
        iconEl.style.color = '';
    });

    const iconEl = document.createElement('span');
    iconEl.className = 'icon inline-flex w-10 h-10 items-center justify-center bg-surf rounded-xl mb-3 text-pri transition-all duration-300';
    iconEl.textContent = icon;

    const titleEl = document.createElement('h3');
    titleEl.className = 'font-semibold text-base mb-2 text-fg';
    titleEl.textContent = title;

    const descEl = document.createElement('p');
    descEl.className = 'text-muted text-sm';
    descEl.textContent = desc;

    card.appendChild(iconEl);
    card.appendChild(titleEl);
    card.appendChild(descEl);
    document.getElementById('features-grid').appendChild(card);
});

// Stats
stats.forEach(([n, suffix, label]) => {
    const box = document.createElement('div');
    box.className = 'bg-white/5 rounded-2xl p-6 text-center transition-all duration-300 cursor-pointer';

    box.addEventListener('mouseenter', () => {
        box.style.transform = 'translateY(-4px)';
        box.style.boxShadow = '0 20px 40px rgba(192,193,255,0.1)';
    });
    box.addEventListener('mouseleave', () => {
        box.style.transform = '';
        box.style.boxShadow = '';
    });

    const valEl = document.createElement('b');
    valEl.className = 'block text-3xl text-pri font-bold';
    valEl.textContent = n + suffix;

    const labelEl = document.createElement('span');
    labelEl.className = 'text-muted text-xs uppercase tracking-wide';
    labelEl.textContent = label;

    box.appendChild(valEl);
    box.appendChild(labelEl);
    document.getElementById('stats-grid').appendChild(box);
});