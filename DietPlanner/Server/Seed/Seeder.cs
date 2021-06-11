using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.StringInfos;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Shared.DesignPatterns.FluentFactory;
using DietPlanner.Shared.StringInfo;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DietPlanner.Server.Seed
{
    public class Seeder
    {
        private readonly DietPlannerDbContext dbContext;
        private readonly Guid systemUserId = Guid.Parse(UserStringInfo.SystemUserId);
        private readonly DateTime now = DateTime.Now;

        public Seeder(DietPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private ValueTask<EntityEntry<Disease>> DiseaseAddAsync(string name) =>
      dbContext.Diseases.AddAsync(FluentFactory<Disease>.Init()
       .GiveAValue(x => x.CreateUserId, systemUserId)
       .GiveAValue(x => x.CreatedTime, now)
       .GiveAValue(x => x.Name, name)
       .Take());
        private ValueTask<EntityEntry<Food>> FoodAddAsync(string name, string description) =>
              dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.Name, name)
               .GiveAValue(x => x.Description, description)
               .Take());
        private ValueTask<EntityEntry<Diet>> DietAddAsync(string name, string description) =>
              dbContext.Diets.AddAsync(FluentFactory<Diet>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.Name, name)
               .GiveAValue(x => x.Description, description)
               .Take());
        private async Task DietFoodAddAsync(EntityEntry<Diet> diet, params EntityEntry<Food>[] foods)
        {
            var dietId = diet.Entity.Id;
            foreach (var item in foods)
                await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                      .GiveAValue(x => x.CreateUserId, systemUserId)
                      .GiveAValue(x => x.CreatedTime, now)
                      .GiveAValue(x => x.DietId, dietId)
                      .GiveAValue(x => x.FoodId, item.Entity.Id)
                      .Take());
        }

        public async Task RoleSeedAsync()
        {
            if (dbContext.Roles.Any())
                return;
            await dbContext.Roles.AddAsync(new() { Name = RoleInfo.Admin });
            await dbContext.Roles.AddAsync(new() { Name = RoleInfo.Patient });
            await dbContext.Roles.AddAsync(new() { Name = RoleInfo.Dietician });
            await dbContext.SaveChangesAsync();
        }

        public async Task UserSeedAsync()
        {
            if (dbContext.Admins.Any())
                return;
            Role adminRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleInfo.Admin));
            Role dieticianRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleInfo.Dietician));


            await dbContext.Admins.AddAsync(FluentFactory<Admin>.Init()
               .GiveAValue(x => x.Address, "Istanbul")
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.Email, "admin@dietplanner.com")
               .GiveAValue(x => x.FirstName, "Admin")
               .GiveAValue(x => x.LastName, "1")
               .GiveAValue(x => x.IdentityNumber, "11111111112")
               .GiveAValue(x => x.Password, "Password12*")
               .GiveAValue(x => x.PhoneNumber, "05319649002")
               .GiveAValue(x => x.Role, adminRole)
               .Use(admin => admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password))
               .Take());


            await dbContext.Dieticians.AddAsync(FluentFactory<Dietician>.Init()
                .GiveAValue(x => x.Address, "Istanbul")
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.Email, "mehmetfaruk@dietplanner.com")
                .GiveAValue(x => x.FirstName, "Mehmet")
                .GiveAValue(x => x.LastName, "Faruk")
                .GiveAValue(x => x.IdentityNumber, "94381916326")
                .GiveAValue(x => x.Password, "MehmetFaruk12*")
                .GiveAValue(x => x.PhoneNumber, "05327933955")
                .GiveAValue(x => x.Role, dieticianRole)
                .Use(dietician => dietician.Password = BCrypt.Net.BCrypt.HashPassword(dietician.Password))
                .Take());

            await dbContext.Dieticians.AddAsync(FluentFactory<Dietician>.Init()
                .GiveAValue(x => x.Address, "Istanbul")
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.Email, "ismailtokmakci@dietplanner.com")
                .GiveAValue(x => x.FirstName, "Ismail")
                .GiveAValue(x => x.LastName, "Tokmakçı")
                .GiveAValue(x => x.IdentityNumber, "48343725006")
                .GiveAValue(x => x.Password, "IsmailTokmakçı12*")
                .GiveAValue(x => x.PhoneNumber, "05329767219")
                .GiveAValue(x => x.Role, dieticianRole)
                .Use(dietician => dietician.Password = BCrypt.Net.BCrypt.HashPassword(dietician.Password))
                .Take());

            await dbContext.Dieticians.AddAsync(FluentFactory<Dietician>.Init()
                .GiveAValue(x => x.Address, "Istanbul")
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.Email, "rakimcelik@dietplanner.com")
                .GiveAValue(x => x.FirstName, "Rakım")
                .GiveAValue(x => x.LastName, "Çelik")
                .GiveAValue(x => x.IdentityNumber, "97485229668")
                .GiveAValue(x => x.Password, "MehmetFaruk12*")
                .GiveAValue(x => x.PhoneNumber, "05326981510")
                .GiveAValue(x => x.Role, dieticianRole)
                .Use(dietician => dietician.Password = BCrypt.Net.BCrypt.HashPassword(dietician.Password))
                .Take());
            await dbContext.SaveChangesAsync();
        }

        public async Task DiseaseSeedAsync()
        {
            if (dbContext.Diseases.Any())
                return;
            await DiseaseAddAsync("Obez");
            await DiseaseAddAsync("Çölyak");
            await DiseaseAddAsync("Şeker");
            await dbContext.SaveChangesAsync();
        }

        public async Task DietSeedAsync()
        {
            if (dbContext.Diets.Any())
                return;

            #region Foods
            var cavdarEkmegi = await FoodAddAsync("Çavdar Ekmeği", "1 dilim tam çavdar ekmeği (erkekler için iki dilim)");
            var zeytinyagi = await FoodAddAsync("Zeytin Yağı", "Yağ");
            var kekik = await FoodAddAsync("Kekik", "Kekik");
            var pulbiber = await FoodAddAsync("Pul Biber", "Pul Biber");
            var tazeFesleyen = await FoodAddAsync("Taze Fesleğen", "Taze Fesleğen");
            var domates = await FoodAddAsync("Domates", "Akdeniz domatesi");
            var yesilbiber = await FoodAddAsync("Yeşil Biber", "Yeşil Biber");
            var maydonoz = await FoodAddAsync("Maydonoz", "Bir tutam Maydanoz");
            var cay = await FoodAddAsync("Çay", "Şekersiz açık çay");
            var karpuz = await FoodAddAsync("Karpuz", "1 dilim karpuz");
            var mercimeksalatasi = await FoodAddAsync("Mercimek Salatası", "1 kâse");
            var peynir = await FoodAddAsync("Peynir", "1 dilim beyaz peynir (erkekler için iki dilim peynir)");
            var lorPeynir = await FoodAddAsync("Lor Peynir", "50 gram lor peyniri");
            var galete = await FoodAddAsync("Galete", "2 kepekli grisini (erkekler için 4 kepekli grisini)");
            var yesilZeytin = await FoodAddAsync("Yeşil Zeytin", "5 yeşil zeytin");
            var siyahZeytin = await FoodAddAsync("Siyah Zeytin", "5 siyah zeytin");
            var kiymaliBezelye = await FoodAddAsync("Kıymalı Bezelye", "6 çorba kaşığı");
            var bulgurPilavi = await FoodAddAsync("Bulgur Pilavı", "3 çorba kaşığı bulgur pilavı (erkekler için 4 çorba kaşığı bulgur pilavı)");
            var cacik = await FoodAddAsync("Cacık", "1 kase cacık");
            var ayran = await FoodAddAsync("Ayran", "Bir bardak ayran");
            var seftali = await FoodAddAsync("Şeftali", "1 adet şeftali");
            var findik = await FoodAddAsync("Fındık", "bir avuç fındık");
            var su = await FoodAddAsync("Su", "bir bardak ılık su");
            var tamTahilliEkmek = await FoodAddAsync("Tam Tahıllı Ekmek", "1 dilim tam tahıllı ekmek");
            var avakadoTost = await FoodAddAsync("Avokadolu Tost", "Avokadolu Tost");
            var madenSuyu = await FoodAddAsync("Maden Suyu", "1 şişe sade maden suyu");
            var haslamaSebze = await FoodAddAsync("Haşlama Sebze", "Taze haşlanmış sebze");
            var sebzeYemegi = await FoodAddAsync("Sebze Yemeği", "Bol sulu sebze yemeği");
            var kinoalıMeyveliSalata = await FoodAddAsync("Kinoalı Meyveli Salata", "Kinoalı Meyveli Salata");
            var haslamaYumurta = await FoodAddAsync("Haşlama Yumurta", "Haşlanmış tavuk yumurtası");
            var tavadaYumurta = await FoodAddAsync("Tavada Yumurta", "Tavada yağla pişmiş yumurta");
            var salatalık = await FoodAddAsync("Salatalık", "Sebze");
            var glutensizEkmek = await FoodAddAsync("Glutensiz Ekmek", "Unlu mamül");
            var tazeMeyve = await FoodAddAsync("Taze Meyve", "Taze mevsim meyvesi");
            var bitkiCayi = await FoodAddAsync("Bitki Çayı", "Açık bitki çayı");
            var izgaraEt = await FoodAddAsync("Izgara Et/Tavuk", "1 Porsiyon Orta Pişmiş");
            var izgaraBalik = await FoodAddAsync("Izgara Balık", "1 porsiyon ızgara balık");
            var bakliyat = await FoodAddAsync("Bakliyat Yemeği", "Kuru fasulye, nohut 1 porsiyon");
            var mevsimSalata = await FoodAddAsync("Mevsim Salata", "Bol yeşillikli salata");
            var pirincPilavi = await FoodAddAsync("Pirinç Pilavı", "3 Kaşık");
            var cevizIci = await FoodAddAsync("Ceviz İçi", "2 ceviz içi");
            var yogurt = await FoodAddAsync("Yoğurt", "Yarım yağlı yoğurt");
            var sut = await FoodAddAsync("Süt", "Yarım yağlı inek sütü");
            var badem = await FoodAddAsync("Badem", "Çiğ Badem");
            var kahve = await FoodAddAsync("Kahve", "Kahve Çeşitleri");
            await dbContext.SaveChangesAsync();
            #endregion

            #region Diyetler
            var akdeniz = await DietAddAsync("Akdeniz", "Akdeniz SABAH 1 dilim tam çavdar ekmeği (erkekler için iki dilim) 50 gram lor peyniri 1 tatlı kaşığı zeytinyağı, kekik, pul biber, taze fesleğen Domates, yeşil biber, maydanoz Şekersiz açık çay ARA ÖĞÜN 1 dilim karpuz ÖĞLE 1 kâse mercimek salatası 1 dilim az yağlı beyaz peynir 1 dilim tam çavdar ekmeği ARA ÖĞÜN 1 dilim peynir (erkekler için iki dilim peynir) 2 kepekli grisini (4 kepekli grisini) 5 yeşil zeytin AKŞAM 6 çorba kaşığı kıymalı bezelye 3 çorba kaşığı bulgur pilavı (erkekler için 4 çorba kaşığı bulgur pilavı) Cacık veya ayran ARA 1 şeftali 10 fındık");
            var gulutensiz = await DietAddAsync("Glutensiz", "Glutensiz Kahvaltı: 1 dilim beyaz peynir 1 adet haşlama veya tavada yumurta 4 adet zeytin + Domates- Salatalık 1-2 dilim glutensiz ekmek Kuşluk: 1 porsiyon taze meyve + Bitki çayı Öğlen: 100-120 gr Izgara et/tavuk veya 1 porsiyon bakliyat yemeği 1 bardak ayran Mevsim salata 3 kaşık pirinç pilavı veya 1 dilim glutensiz ekmek (25 gr) İkindi: 1 porsiyon taze meyve + 2 adet ceviz içi Akşam: 1 porsiyon sebze yemeği 1 kase yoğurt 1-2 dilim glutensiz ekmek Gece: 1 bardak süt + 5-6 adet çiğ badem");
            var denizUrunleri = await DietAddAsync("Deniz Ürünleri", "Deniz Ürünleri Sabah Kalkınca: 1 bardak ılık su KAHVALTI 1.Kahvaltı Menüsü 1 dilim beyaz peynir + 1 adet haşlanmış yumurta + 5-6  adet zeytin + Bol yeşillik + 1 dilim tam tahıllı ekmek 2. Kahvaltı Menüsü Avokadolu tost + 1 porsiyon meyve + Bol yeşillik ÖĞLE YEMEĞİ 1. Öğle Yemeği Menüsü Izgara balık + haşlama sebze + maden suyu 2. Öğle Yemeği Menüsü 1 porsiyon sebze yemeği +  1 kase yoğurt + 1 dilim ekmek AKŞAM YEMEĞİ 1. Akşam Yemeği Menüsü Kinoalı meyveli  salata 2. Akşam Yemeği Menüsü 1 porsiyon kurubaklagil yemeği + 3-4 yemek kaşığı bulgur pilavı + 1 kase salata (z.yağı ve limon ile) ARA ÖĞÜN ÖNERİLERİ 2 adet ceviz veya 5-6 adet badem/fındık 1 porsiyon meyve Bitki çayları Kahve çeşitleri");

            await dbContext.SaveChangesAsync();
            #endregion

            #region Akdeniz
            await DietFoodAddAsync(diet: akdeniz, cavdarEkmegi, lorPeynir, zeytinyagi, kekik, pulbiber, tazeFesleyen, domates,
                yesilbiber, maydonoz, cay, karpuz, mercimeksalatasi, peynir, galete, yesilZeytin, kiymaliBezelye,
                bulgurPilavi, cacik, ayran, seftali, findik);
            #endregion

            #region Gulitensiz
            await DietFoodAddAsync(diet: gulutensiz, peynir, haslamaYumurta, tavadaYumurta, domates, salatalık, glutensizEkmek,
                tazeMeyve, bitkiCayi, izgaraEt, bakliyat, cevizIci, ayran, mevsimSalata, pirincPilavi,
                sebzeYemegi, yogurt, sut, badem);
            #endregion

            #region Deniz Urunleri
            await DietFoodAddAsync(diet: denizUrunleri, su, peynir, haslamaYumurta, siyahZeytin, tamTahilliEkmek, avakadoTost,
                tazeMeyve, izgaraBalik, madenSuyu, haslamaSebze, yogurt, kinoalıMeyveliSalata, bakliyat, bulgurPilavi,
                mevsimSalata, cevizIci, badem, findik, tazeMeyve, bitkiCayi, kahve);
            #endregion

            await dbContext.SaveChangesAsync();
        }

        public async Task SeedAsync()
        {
            await RoleSeedAsync();
            await UserSeedAsync();
            await DiseaseSeedAsync();
            await DietSeedAsync();

        }
    }
}