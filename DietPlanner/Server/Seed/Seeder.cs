using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.StringInfos;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Shared.DesignPatterns.FluentFactory;
using DietPlanner.Shared.StringInfo;

using Microsoft.EntityFrameworkCore;

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
            await dbContext.Diseases.AddAsync(FluentFactory<Disease>.Init()
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.Name, "Obez")
                .Take());

            await dbContext.Diseases.AddAsync(FluentFactory<Disease>.Init()
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.Name, "Çölyak")
                .Take());

            await dbContext.Diseases.AddAsync(FluentFactory<Disease>.Init()
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.Name, "Şeker")
                .Take());
            await dbContext.SaveChangesAsync();
        }

        public async Task DietSeedAsync()
        {
            if (dbContext.Diets.Any())
                return;

            #region Foods
            var cavdarEkmegi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Çavdar Ekmeği")
            .GiveAValue(x => x.Description, "1 dilim tam çavdar ekmeği (erkekler için iki dilim)")
            .Take());

            var zeytinyagi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Zeytin Yağı")
            .GiveAValue(x => x.Description, "Yağ")
            .Take());

            var kekik = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Kekik")
            .GiveAValue(x => x.Description, "Kekik")
            .Take());

            var pulbiber = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Pul Biber")
            .GiveAValue(x => x.Description, "Pul Biber")
            .Take());

            var tazeFesleyen = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Taze Fesleğen")
            .GiveAValue(x => x.Description, "Taze Fesleğen")
            .Take());

            var domates = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Domates")
            .GiveAValue(x => x.Description, "Akdeniz domatesi")
            .Take());

            var yesilbiber = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Yeşil Biber")
            .GiveAValue(x => x.Description, "Yeşil Biber")
            .Take());

            var maydonoz = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Maydonoz")
            .GiveAValue(x => x.Description, "Bir tutam Maydanoz")
            .Take());

            var cay = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Çay")
            .GiveAValue(x => x.Description, "Şekersiz açık çay")
            .Take());

            var karpuz = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Karpuz")
            .GiveAValue(x => x.Description, "1 dilim karpuz")
            .Take());

            var mercimeksalatasi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Mercimek Salatası")
            .GiveAValue(x => x.Description, "1 kâse")
            .Take());

            var peynir = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Peynir")
            .GiveAValue(x => x.Description, "1 dilim beyaz peynir (erkekler için iki dilim peynir)")
            .Take());

            var lorPeynir = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Lor Peynir")
            .GiveAValue(x => x.Description, "50 gram lor peyniri")
            .Take());


            var galete = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Galete")
            .GiveAValue(x => x.Description, "2 kepekli grisini (erkekler için 4 kepekli grisini)")
            .Take());

            var yesilZeytin = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Yeşil Zeytin")
            .GiveAValue(x => x.Description, "5 yeşil zeytin")
            .Take());

            var siyahZeytin = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Siyah Zeytin")
            .GiveAValue(x => x.Description, "5 siyah zeytin")
            .Take());

            var kiymaliBezelye = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Kıymalı Bezelye")
            .GiveAValue(x => x.Description, "6 çorba kaşığı")
            .Take());

            var bulgurPilavi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Bulgur Pilavı")
            .GiveAValue(x => x.Description, "3 çorba kaşığı bulgur pilavı (erkekler için 4 çorba kaşığı bulgur pilavı)")
            .Take());

            var cacik = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Cacık")
            .GiveAValue(x => x.Description, "1 kase cacık")
            .Take());

            var ayran = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Ayran")
            .GiveAValue(x => x.Description, "bir bardak ayran")
            .Take());

            var seftali = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Şeftali")
            .GiveAValue(x => x.Description, "1 adet şeftali")
            .Take());

            var findik = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Fındık")
            .GiveAValue(x => x.Description, "bir avuç fındık")
            .Take());

            var su = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Su")
            .GiveAValue(x => x.Description, "bir bardak ılık su")
            .Take());

            var tamTahilliEkmek = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Tam Tahıllı Ekmek")
            .GiveAValue(x => x.Description, "1 dilim tam tahıllı ekmek")
            .Take());

            var avakadoTost = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Avokadolu Tost")
            .GiveAValue(x => x.Description, "Avokadolu tost")
            .Take());

            var madenSuyu = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Maden Suyu")
            .GiveAValue(x => x.Description, "1 şişe sade maden suyu")
            .Take());

            var haslamaSebze = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Haşlama Sebze")
            .GiveAValue(x => x.Description, "Taze haşlanmış sebze")
            .Take());

            var kinoalıMeyveliSalata = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Kinoalı Meyveli Salata")
            .GiveAValue(x => x.Description, "Kinoalı meyveli salata")
            .Take());

            var haslamaYumurta = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Haşlama Yumurta")
            .GiveAValue(x => x.Description, "Haşlanmış tavuk yumurtası")
            .Take());

            var tavadaYumurta = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, DateTime.Now)
            .GiveAValue(x => x.Name, "Tavada Yumurta")
            .GiveAValue(x => x.Description, "Tavada yağla pişmiş yumurta")
            .Take());

            var salatalık = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Salatalık")
            .GiveAValue(x => x.Description, "Sebze")
            .Take());

            var glutensizEkmek = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Glutensiz Ekmek")
            .GiveAValue(x => x.Description, "Unlu mamül")
            .Take());

            var tazeMeyve = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Taze Meyve")
            .GiveAValue(x => x.Description, "Taze mevsim meyvesi")
            .Take());

            var bitkiCayi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Bitki Çayı")
            .GiveAValue(x => x.Description, "Açık Çay")
            .Take());

            var izgaraEt = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Izgara Et/Tavuk")
            .GiveAValue(x => x.Description, "1 Porsiyon Orta Pişmiş")
            .Take());

            var izgaraBalik = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
           .GiveAValue(x => x.CreateUserId, systemUserId)
           .GiveAValue(x => x.CreatedTime, now)
           .GiveAValue(x => x.Name, "Izgara Balık")
           .GiveAValue(x => x.Description, "1 porsiyon ızgara balık")
           .Take());

            var bakliyat = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Bakliyat Yemeği")
            .GiveAValue(x => x.Description, "Kuru fasulye, nohut 1 porsiyon")
            .Take());

            var mevsimSalata = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Mevsim Salata")
            .GiveAValue(x => x.Description, "Bol yeşillikli salata")
            .Take());

            var pirincPilavi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Pirinç Pilavı")
            .GiveAValue(x => x.Description, "3 Kaşık")
            .Take());

            var cevizIci = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Ceviz İçi")
            .GiveAValue(x => x.Description, "2 ceviz içi")
            .Take());

            var yogurt = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Yoğurt")
            .GiveAValue(x => x.Description, "Yarım yağlı yoğurt")
            .Take());

            var sebzeYemegi = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Sebze Yemeği")
            .GiveAValue(x => x.Description, "Bol sulu sebze yemeği")
            .Take());

            var sut = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Süt")
            .GiveAValue(x => x.Description, "Yarım yağlı inek sütü")
            .Take());

            var badem = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Badem")
            .GiveAValue(x => x.Description, "Çiğ Badem")
            .Take());

            var kahve = await dbContext.Foods.AddAsync(FluentFactory<Food>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Kahve")
            .GiveAValue(x => x.Description, "Kahve Çeşitleri")
            .Take());
            #endregion

            await dbContext.SaveChangesAsync();


            var akdeniz = await dbContext.Diets.AddAsync(FluentFactory<Diet>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Akdeniz")
            .GiveAValue(x => x.Description, "Akdeniz SABAH 1 dilim tam çavdar ekmeği (erkekler için iki dilim) 50 gram lor peyniri 1 tatlı kaşığı zeytinyağı, kekik, pul biber, taze fesleğen Domates, yeşil biber, maydanoz Şekersiz açık çay ARA ÖĞÜN 1 dilim karpuz ÖĞLE 1 kâse mercimek salatası 1 dilim az yağlı beyaz peynir 1 dilim tam çavdar ekmeği ARA ÖĞÜN 1 dilim peynir (erkekler için iki dilim peynir) 2 kepekli grisini (4 kepekli grisini) 5 yeşil zeytin AKŞAM 6 çorba kaşığı kıymalı bezelye 3 çorba kaşığı bulgur pilavı (erkekler için 4 çorba kaşığı bulgur pilavı) Cacık veya ayran ARA 1 şeftali 10 fındık")
            .Take());



            var gulutensiz = await dbContext.Diets.AddAsync(FluentFactory<Diet>.Init()
            .GiveAValue(x => x.CreateUserId, systemUserId)
            .GiveAValue(x => x.CreatedTime, now)
            .GiveAValue(x => x.Name, "Glutensiz")
            .GiveAValue(x => x.Description, "Glutensiz Kahvaltı: 1 dilim beyaz peynir 1 adet haşlama veya tavada yumurta 4 adet zeytin + Domates- Salatalık 1-2 dilim glutensiz ekmek Kuşluk: 1 porsiyon taze meyve + Bitki çayı Öğlen: 100-120 gr Izgara et/tavuk veya 1 porsiyon bakliyat yemeği 1 bardak ayran Mevsim salata 3 kaşık pirinç pilavı veya 1 dilim glutensiz ekmek (25 gr) İkindi: 1 porsiyon taze meyve + 2 adet ceviz içi Akşam: 1 porsiyon sebze yemeği 1 kase yoğurt 1-2 dilim glutensiz ekmek Gece: 1 bardak süt + 5-6 adet çiğ badem")
            .Take());


            var denizUrunleri = await dbContext.Diets.AddAsync(FluentFactory<Diet>.Init()
           .GiveAValue(x => x.CreateUserId, systemUserId)
           .GiveAValue(x => x.CreatedTime, now)
           .GiveAValue(x => x.Name, "Deniz Ürünleri")
           .GiveAValue(x => x.Description, "Deniz Ürünleri Sabah Kalkınca: 1 bardak ılık su KAHVALTI 1.Kahvaltı Menüsü 1 dilim beyaz peynir + 1 adet haşlanmış yumurta + 5-6  adet zeytin + Bol yeşillik + 1 dilim tam tahıllı ekmek 2. Kahvaltı Menüsü Avokadolu tost + 1 porsiyon meyve + Bol yeşillik ÖĞLE YEMEĞİ 1. Öğle Yemeği Menüsü Izgara balık + haşlama sebze + maden suyu 2. Öğle Yemeği Menüsü 1 porsiyon sebze yemeği +  1 kase yoğurt + 1 dilim ekmek AKŞAM YEMEĞİ 1. Akşam Yemeği Menüsü Kinoalı meyveli  salata 2. Akşam Yemeği Menüsü 1 porsiyon kurubaklagil yemeği + 3-4 yemek kaşığı bulgur pilavı + 1 kase salata (z.yağı ve limon ile) ARA ÖĞÜN ÖNERİLERİ 2 adet ceviz veya 5-6 adet badem/fındık 1 porsiyon meyve Bitki çayları Kahve çeşitleri")
           .Take());


            await dbContext.SaveChangesAsync();

            var akdenizId = akdeniz.Entity.Id;
            var gulutensizId = gulutensiz.Entity.Id;
            var denizUrunleriId = denizUrunleri.Entity.Id;



            #region Akdeniz
            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                   .GiveAValue(x => x.CreateUserId, systemUserId)
                   .GiveAValue(x => x.CreatedTime, now)
                   .GiveAValue(x => x.DietId, akdenizId)
                   .GiveAValue(x => x.FoodId, cavdarEkmegi.Entity.Id)
                   .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                .GiveAValue(x => x.CreateUserId, systemUserId)
                .GiveAValue(x => x.CreatedTime, now)
                .GiveAValue(x => x.DietId, akdenizId)
                .GiveAValue(x => x.FoodId, lorPeynir.Entity.Id)
                .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.DietId, akdenizId)
               .GiveAValue(x => x.FoodId, zeytinyagi.Entity.Id)
               .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.DietId, akdenizId)
               .GiveAValue(x => x.FoodId, kekik.Entity.Id)
               .Take());


            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.DietId, akdenizId)
               .GiveAValue(x => x.FoodId, pulbiber.Entity.Id)
               .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, tazeFesleyen.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, domates.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, yesilbiber.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, maydonoz.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, cay.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, karpuz.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, mercimeksalatasi.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, peynir.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, galete.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, yesilZeytin.Entity.Id)
              .Take());


            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, kiymaliBezelye.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, bulgurPilavi.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, cacik.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, ayran.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, seftali.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, akdenizId)
              .GiveAValue(x => x.FoodId, findik.Entity.Id)
              .Take());
            #endregion


            #region Gulitensiz
            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, peynir.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, haslamaYumurta.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, tavadaYumurta.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, domates.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, salatalık.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, glutensizEkmek.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, tazeMeyve.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, bitkiCayi.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, izgaraEt.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, bakliyat.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, cevizIci.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, ayran.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, mevsimSalata.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, pirincPilavi.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, sebzeYemegi.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, yogurt.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, sut.Entity.Id)
                 .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
                 .GiveAValue(x => x.CreateUserId, systemUserId)
                 .GiveAValue(x => x.CreatedTime, now)
                 .GiveAValue(x => x.DietId, gulutensizId)
                 .GiveAValue(x => x.FoodId, badem.Entity.Id)
                 .Take());
            #endregion

            #region Deniz Urunleri
            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.DietId, denizUrunleriId)
               .GiveAValue(x => x.FoodId, su.Entity.Id)
               .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, peynir.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, haslamaYumurta.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, siyahZeytin.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, tamTahilliEkmek.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, avakadoTost.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, tazeMeyve.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, izgaraBalik.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, madenSuyu.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, sebzeYemegi.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, yogurt.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, kinoalıMeyveliSalata.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, bakliyat.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, bulgurPilavi.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, mevsimSalata.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, cevizIci.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, badem.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, findik.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, tazeMeyve.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, bitkiCayi.Entity.Id)
              .Take());

            await dbContext.DietFoods.AddAsync(FluentFactory<DietFood>.Init()
              .GiveAValue(x => x.CreateUserId, systemUserId)
              .GiveAValue(x => x.CreatedTime, now)
              .GiveAValue(x => x.DietId, denizUrunleriId)
              .GiveAValue(x => x.FoodId, kahve.Entity.Id)
              .Take());
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
