# Silverkeep için araştırılan yeni özellikler

Bu dosya öneri listesidir; aşağıdaki yeni özellikler henüz uygulamaya eklenmedi.

## Üçüncü taraf araçlardan örnekler

1. **Salvage / transmute / artifact melding hesabı.** Ekipmanı parçalamak, girdiyi yükseltmek veya artifact üretmek ile satmak arasındaki fark. Kaynak: [Profit Forge](https://albionprofitforge.com/guide). Market fiyatları mevcut AODP API’sinden alınabilir; tarif, silver gideri ve rastgele çıktı olasılıkları ayrıca doğrulanmalı. Melding için tek işlem kâr garantisi yerine beklenen değer ve kayıp aralığı gerekir.
2. **Balık işleme karşılaştırması.** Ham sat, chopped fish / sauce yap veya yemekte kullan seçeneklerini aynı tabloda karşılaştır. Kaynaklar: [Profit Forge](https://albionprofitforge.com/guide), [AlbionOnlineGrind](https://albiononlinegrind.com/chopped-fish-calculator). Fiyatlar API’den, dönüşüm miktarları doğrulanmış oyun verisinden.
3. **Faction / heart-run hesabı.** Kalp taşıma girdileri, silver masrafı, beklenen çıktı ve faction point dönüşümünü karşılaştır. [AlbionKit](https://www.albionkit.com/) bu araç ailesini sunuyor. Silver piyasası API ile, kişisel puan ve süre girdileri yerel kayıtla yapılabilir. Canlı Bandit olay API’si doğrulanmadan canlı takip sözü verilmemeli.
4. **Fiyat güvenilirliği ve şişirilmiş fiyat filtresi.** Güncel ilanı geçmiş referansa ve diğer şehirlere göre kontrol et; aykırı fiyatı açıkça işaretle. [Profit Forge](https://albionprofitforge.com/guide) benzer kontrolleri açıklıyor. AODP fiyat geçmişi kullanılabilir; geçmiş kayıt miktarı kesin satış likiditesi değildir.
5. **Oyuncu / guild / killboard sekmesi.** [AlbionKit](https://www.albionkit.com/) oyuncu, savaş ve guild araçları sunuyor. Europe oyun bilgi uç noktalarının erişimi ve veri alanları ayrı test edilmeli; bunu ekonomik modüllerden sonra ele almak daha uygun.

## Silverkeep’e özel daha faydalı eklemeler

- **Hedef kâra göre maksimum loot teklifi:** “Bu tab’dan en az 150k net istiyorum” yaz; vergi, emir açma bedeli ve masraf sonrası en yüksek ödeme tutarını ve yüzdesini hesapla. Mevcut yüzde hesabının tersini çözer.
- **Lot bazlı stok muhasebesi:** aynı eşyayı farklı tarihlerde/fiyatlarda al; hangi partiden kaç adet sattığını seç. FIFO veya ağırlıklı ortalamayla gerçekleşmiş kâr göster. Alış emirleri ve depo bağlantıları ancak kullanıcı işlemi doğruladığında yapılmalı.
- **Minimum stok ve tek alışveriş listesi:** craft kuyruğu, build sepeti ve seçtiğin minimum depo hedeflerini birleştir; eksikleri şehir bazında göster. Aynı malzemeyi iki kez saymayı engelle.
- **Premium gider hedefi:** kullanıcının girdiği güncel Premium silver maliyetini aylık gider hedefi yap; gerçek kâr defterinden kalan tutarı, gerekli silver/gün ve geçmiş oturumlarına göre tahmini çalışma süresini göster.
- **Alım öncesi fiyat kilidi:** teklif oluştururken kullanılan fiyatları ve saatlerini işlemle sakla. Sonradan API değişse de eski teklifin hesabı değişmesin; hedef/gerçek satış farkı görülsün.
- **Hızlı işlem ve geri al:** son stok hareketini güvenli biçimde ters hareketle düzelt; işlem geçmişini silmek yerine düzeltme kaydı üret. Çok eşyalı loot listesine toplu adet/fiyat girişi ekle.

Önerilen sıra: **maksimum loot teklifi → lot bazlı stok kârı → minimum stok/listeler → fiyat güvenilirliği → balık/salvage → faction/killboard.** İlk üçü mevcut kullanımına doğrudan uyar ve ek veri sağlayıcı gerektirmez.

Araştırma kaynakları: [AODP üçüncü taraf araçlar](https://www.albion-online-data.com/3rd-party-tools), [AODP API](https://www.albion-online-data.com/api/). Araçların kendi tanıtım sayfaları özellik örneği olarak kullanıldı; hesapları veya kaynak kodları kopyalanmadı.
