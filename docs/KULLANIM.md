# Silverkeep 4.0 · xvv

Windows masaüstü uygulaması. Europe varsayılan sunucudur; Americas ve Asia ayarlardan seçilebilir. `Silverkeep-Kurulum.exe` tek tıkla kurar ve masaüstüne kısayol ekler.

## Yeni çalışma alanları

- **Kârlı craft taraması:** tier, kategori, 20–160 eşyalık kapsam, bütçe, iade ve istasyon/taşıma bedeli. Güncel fiyatlarla şehir rotaları net kazanca göre sıralanır. “Planla” hesap seçeneklerini craft ekranına taşır. Tarama durdurulabilir; tüm kütüphane aynı anda taranmaz.
- **Fiyat geçmişi:** Market ekranında şehrin “Geçmiş” düğmesi. 7 ve 30 günlük, API kayıt miktarıyla ağırlıklandırılmış ortalamalar ve güncel fiyat sapması. Geçmiş veri satış ilanları içindir; gerçek dolum veya satış garantisi değildir.
- **Üretim yolu:** doğrudan alım/craft ile daha düşük enchant seviyesinden yükseltme; tek şehir veya malzemeleri farklı şehirlerden alma. Beklenen malzeme iadesi kullanılır. İlk üretim için brüt malzeme ve sermaye gerekir. Taşıma maliyetini kendin gir.
- **Taşıma:** eşya ağırlığı, kullanılabilir binek kapasitesi, sefer sayısı, süre, masraf, ekipman kaybı ve girdiğin kayıp olasılığı. Risk canlı tehlike verisi değildir. API düğmesi anında alış/anında satış fiyatını getirir.
- **Adalar:** tarla/ot veya hayvan döngüsü; girdi/çıktı eşya seçimi, API fiyatları, verim, tohum/yavru geri dönüşü, yem, gider, focus ve döngü süresi. Verim değerlerini oyun içinden gir. Başlat düğmesi hasat hatırlatıcısı oluşturur.
- **Sayaçlar:** elle süreli hatırlatıcı; uygulama sistem tepsisinde çalışırken Windows bildirimi. Bilgisayar uyurken veya uygulama kapalıyken bildirim çalışmaz; gecikmiş sayaçlar yeniden açılışta kontrol edilir.
- **İşçi & journal:** boş/dolu journal API fiyatları, beklenen çıktı değeri ve verim; journal satışı ile işçiye verme karşılaştırması, ev yatırımının geri dönüşü. Craft’ın temel fame miktarıyla journal katkısı hesaplanıp craft ekranına eklenebilir. Premium fame bonusu journal dolumuna otomatik eklenmez.
- **Spec & focus:** karakterinin spec/mastery profilleri yerelde saklanır. Oyundaki toplam focus efficiency ve tarifin temel focus’u ile maliyet hesaplanır; 10.000 efficiency temel maliyeti yarıya indirir. Focus’lu/Focus’suz net farkla ek silver/focus karşılaştırması. Profilin maliyetini craft’a uygulayabilirsin. Destiny Board otomatik okunmaz.
- **Kâr defteri:** gerçekleşmiş gelir/gider, tarih, kategori, vergi, taşıma veya ölüm zararı. Tahmini API kârı otomatik gerçek kâr yazılmaz. Satış zaten vergi sonrasıysa vergiyi tekrar yazma.
- **Gathering raporu:** tamamlanmış kendi oturumlarına göre bölge neti, silver/saat ve kaynak karşılaştırması. Bir oturumda birden fazla kaynak varsa kaynak hızı tüm oturum süresine bölünür; kaynak başında geçirilen ayrı süre bilinmez.
- **Loot paylaşımı:** ortak gider sonrası eşit veya ağırlıklı dağıtım; tam silver’a yuvarlama farkı son katılımcıya eklenir. Paylaşım geçmişi yerelde saklanır.
- **Avalon rota defteri:** elle girilen portal bağlantıları, görsel bağlantı haritası, kapanış zamanları, notlar ve kapanış alarmı. Süresi geçmiş bağlantılar kesiklidir; otomatik portal okuması yoktur.

## Önceki özellikler

Market, craft/refining, gerçek iadelerle üretimi depoya işleme, çoklu depo, transfer, CSV, üretim planları ve alışveriş listesi, gathering oturumları, şehir fırsatları, fiyat alarmları, %85 varsayılan loot alım hesabı, build sepeti, emir defteri, enchant ve altın API’si korunmuştur.

## Kayıtlar ve güncelleme

AlbionDesk kayıtların korunur. Uyumluluk için kayıt klasörü `%APPDATA%\AlbionDesk` ve mevcut kurulum konumu `%LOCALAPPDATA%\Programs\AlbionDesk` olarak kalır. Uygulamanın görünen adı, çalıştırılabilir dosyası ve masaüstü kısayolu Silverkeep’tir.

`records.json` yerel kayıt, `records.json.bak` önceki sağlam kayıt, `backups/` son 30 günün yedekleri, `market-cache.json` API önbelleğidir. Ayarlardan JSON dışa/içe aktarımı yapılabilir. Kaldırma takip kayıtlarını silmez. Pencereyi kapatmak uygulamayı tepsiye küçültür; tamamen çıkmak için tepsi menüsünü kullan.

## Kaynaklar

Fiyatlar ve geçmiş: https://www.albion-online-data.com/api/ . Eşya ve tarifler: https://github.com/ao-data/ao-bin-dumps . Journal bilgileri: https://wiki.albiononline.com/wiki/Journal . Focus formülü için araç geliştiricisinin açıklaması: https://albionfreemarket.com/articles/view/how-crafting-specialization-reduces-focus-costs-in-albion-online . Özellik araştırması: https://www.albion-online-data.com/3rd-party-tools . Bu bağımsız uygulama Sandbox Interactive tarafından yayınlanmaz. xvv imzaları uygulama kodundadır; oyun verileri, fontlar ve Electron kendi sahiplerinindir.

## 3.0 masaüstü arayüzü

Uygulama Market ekranıyla açılır. Üstte ana bölümler, hemen altında o bölümün araç sekmeleri bulunur. Market ekranında soldaki listeden eşya seçilir; ad/kod araması ve tier filtresi kullanılabilir. Sağda şehir fiyatları, Grafik düğmeleri ve geçmiş karşılaştırması yer alır. İşlem masasında takip listesi, depo hareketleri ve üretim kuyruğu birlikte görünür. Loot alımı hesap formu ile geçmiş tablosu yan yanadır. F1 Market, F2 Craft, F3 Depo, F4 Gathering; Ctrl+K genel eşya aramasıdır. Açık/koyu görünüm ayarlardan seçilebilir.

## 3.1 logo, okunabilirlik ve dil

Yeni S / silver madeni para simgesi pencere, tepsi, masaüstü ve kurulumda kullanılır. Tablo, menü, açıklama ve eşya adları büyütüldü; koyu temadaki metin kontrastı artırıldı. Üst sağdaki Türkçe / English seçimi yerelde saklanır. Menü, hesap formları, yardım metinleri, açılır pencereler, eşya adları, sayı/tarih biçimleri ve tepsi menüsü seçilen dili kullanır. Kullanıcının yazdığı karakter, depo, rota ve oturum adları ile alan değerleri değiştirilmez.

## 4.0 bağlantılı işlemler ve sade Black Market

Uygulama doğrudan **Black Market** ile açılır. Eşya, craft adedi ve malzeme şehrini seç; **Kazancı hesapla** düğmesi Europe fiyatlarıyla net sonucu gösterir. Satış yeri otomatik Black Market, satış şekli anında satıştır. İade/focus/giderleri genişletip oyun değerlerini gir. Eski satış veya malzeme fiyatları işaretlenir; API emir adedi ve gerçekleşme garantisi sağlamaz. **Kârlı ürün bul** tarayıcıyı Black Market filtresiyle açar.

- **Hesaplar → Toplu loot:** eşya, adet ve kalite listesi; API veya manuel birim fiyat, toplam ve yüzde teklif. Eksik fiyatla toplam hesaplanmaz. Kaydedilen tab kendi fiyat ve tarihlerini korur; yüklendiğinde bunlar manuel fiyat olarak gelir. Güncel API’ye dönmek için manuel kutuları boşalt.
- **Depo → Depolar:** Satış kaydet düğmesi stoğu düşürür; gerçek fiyat, vergi, emir bedeli ve giderle satış kârını hesaplar. Net gelir ile stok maliyeti/gideri deftere ayrı, aynı satış kimliğiyle yazılır. Aynı satışı veya maliyetini manuel tekrar kaydetme. Finans ekranı otomatik stok satış kârını manuel farktan ayrı gösterir.
- **Depo → İşlem geçmişi:** 4.0’dan itibaren stok, satış, gerçek craft, gathering ve CSV hareketleri geri alınabilir. Stok ve satış kayıtları adet/fiyatla düzeltilebilir. Eski kayıt ters işlemle korunur; bağlı finans kayıtları iptal işaretiyle görünür kalır. Aynı kaydı değiştiren yeni bir işlem varsa önce onu geri al. Geçersiz düzeltme diske hiçbir değişiklik yazmaz. Önceki sürüm hareketleri yalnızca eski defterde kalır, geçmişe dönük geri alma oluşturulmaz.
- **Üretim → Şablonlar:** craft/refining, Black Market ve taşıma ayarlarını isimle sakla. Fiyatları kullanmadan önce yenile. Refine şablonu seçilen kaynak ve tier’ı korur.
- **Üretim → Üretim kontrolü:** depodaki brüt malzeme, eksikler, tüm girdileri satın alma sermayesi ve focus yeterliliği. İade beklentisi ilk craft’ın brüt ihtiyacını azaltmaz.
- **Depo → Bekleyen stok:** stok satırının başlangıç tarihi, bekleme günleri ve bağlı ağırlıklı kayıt maliyeti. Eski hareketlerden türetilen tarihler tahmin olarak etiketlenir; bilinmeyen tarih boş kalır. Bu takip ayrı alış lotlarının yaşı değildir.
- **Üst sağ → Hızlı hesap:** ayrı küçük Windows penceresi; yüzde/loot hesabı ve kalıcı **Her zaman üstte** seçeneği. Ana uygulamadan bağımsız kapatılabilir.
- **Hesaplar → Hedefler:** tarihli net kâr, kaynak adedi veya belirli eşya için silver birikimi. Kâr, gerçek stok satışları ve satışa bağlı olmayan manuel işlemlerden; gathering kendi oturumlarından; birikim elle girilen tutardan ilerler.
