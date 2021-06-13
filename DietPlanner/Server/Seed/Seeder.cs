using System;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.StringInfos;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Entities.Interfaces;
using DietPlanner.Shared.DesignPatterns.FluentFactory;
using DietPlanner.Shared.StringInfo;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DietPlanner.Server.Seed
{
    internal record UserCreate(string Email, string Address, string FirstName, string LastName, string PhoneNumber, string IdentityNumber, string Password, Role Role);

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
        private ValueTask<EntityEntry<T>> UserAddAsync<T>(UserCreate user) where T : class, IPerson, new() =>
             dbContext.Set<T>().AddAsync(FluentFactory<T>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.Address, user.Address)
               .GiveAValue(x => x.Email, user.Email)
               .GiveAValue(x => x.FirstName, user.FirstName)
               .GiveAValue(x => x.LastName, user.LastName)
               .GiveAValue(x => x.IdentityNumber, user.IdentityNumber)
               .GiveAValue(x => x.Password, user.Password)
               .GiveAValue(x => x.PhoneNumber, user.PhoneNumber)
               .GiveAValue(x => x.Role, user.Role)
               .Use(instance => instance.Password = BCrypt.Net.BCrypt.HashPassword(instance.Password))
               .Take());
        private ValueTask<EntityEntry<Role>> RoleAddAsync(string name) =>
              dbContext.Roles.AddAsync(FluentFactory<Role>.Init()
               .GiveAValue(x => x.CreateUserId, systemUserId)
               .GiveAValue(x => x.CreatedTime, now)
               .GiveAValue(x => x.Name, name)
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
            await RoleAddAsync(RoleInfo.Admin);
            await RoleAddAsync(RoleInfo.Patient);
            await RoleAddAsync(RoleInfo.Dietician);
            await dbContext.SaveChangesAsync();
        }

        public async Task UserSeedAsync()
        {
            if (!dbContext.Admins.Any())
            {
                Role adminRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleInfo.Admin));
                await UserAddAsync<Admin>(new("admin@dietplanner.com", "Istanbul", "Admin",
          "1", "05319649002", "11111111112", "Password12*", adminRole));
            }
            if (!dbContext.Dieticians.Any())
            {
                Role dieticianRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleInfo.Dietician));

                await UserAddAsync<Dietician>(new("mehmetfaruk@dietplanner.com", "Istanbul", "Mehmet",
                    "Faruk", "05327933955", "94381916326", "MehmetFaruk12*", dieticianRole));

                await UserAddAsync<Dietician>(new("rakimcelik@dietplanner.com", "Istanbul", "Rakım",
                       "Çelik", "05326981510", "97485229668", "MehmetFaruk12*", dieticianRole));

                await UserAddAsync<Dietician>(new("nisang@dietplanner.com", "İzmir", "Nisan",
                      "Göksel", "05329767219", "48343725006", "NisanGoksel12*", dieticianRole));
            }


            if (!dbContext.Patients.Any())
            {
                Role patientRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleInfo.Patient));

                await UserAddAsync<Patient>(new("67rsen00@gmail.com", "Istanbul", "Recep",
                    "Şen", "05319649002", "22222222222", "Password12*", patientRole));
            }


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
            var diyetEkmek = await FoodAddAsync("Diyet Ekmeği", "Dilimlenmiş diyet ekmek");
            var marul = await FoodAddAsync("Marul", "1 demet marul");
            var elma = await FoodAddAsync("Elma", "Taze elma");
            var yesillikSalata = await FoodAddAsync("Yeşillikli salata", "Bol yeşillikli salata");
            var corba = await FoodAddAsync("Çorba", "1 kase çorba çeşitleri");
            var diyetYogurt = await FoodAddAsync("Diyet yoğurt", "150 gram diyet yoğurt");
            var erik = await FoodAddAsync("Erik", "2 adet erik");
            var kayısı = await FoodAddAsync("Kayısı", "1 adet kayısı");
            var armut = await FoodAddAsync("Armut", "1 adet taze armut");
            await dbContext.SaveChangesAsync();
            #endregion

            #region Diyetler
            var akdeniz = await DietAddAsync("Akdeniz", "Akdeniz SABAH 1 dilim tam çavdar ekmeği (erkekler için iki dilim) 50 gram lor peyniri 1 tatlı kaşığı zeytinyağı, kekik, pul biber, taze fesleğen Domates, yeşil biber, maydanoz Şekersiz açık çay ARA ÖĞÜN 1 dilim karpuz ÖĞLE 1 kâse mercimek salatası 1 dilim az yağlı beyaz peynir 1 dilim tam çavdar ekmeği ARA ÖĞÜN 1 dilim peynir (erkekler için iki dilim peynir) 2 kepekli grisini (4 kepekli grisini) 5 yeşil zeytin AKŞAM 6 çorba kaşığı kıymalı bezelye 3 çorba kaşığı bulgur pilavı (erkekler için 4 çorba kaşığı bulgur pilavı) Cacık veya ayran ARA 1 şeftali 10 fındık");
            var gulutensiz = await DietAddAsync("Glutensiz", "Glutensiz Kahvaltı: 1 dilim beyaz peynir 1 adet haşlama veya tavada yumurta 4 adet zeytin + Domates- Salatalık 1-2 dilim glutensiz ekmek Kuşluk: 1 porsiyon taze meyve + Bitki çayı Öğlen: 100-120 gr Izgara et/tavuk veya 1 porsiyon bakliyat yemeği 1 bardak ayran Mevsim salata 3 kaşık pirinç pilavı veya 1 dilim glutensiz ekmek (25 gr) İkindi: 1 porsiyon taze meyve + 2 adet ceviz içi Akşam: 1 porsiyon sebze yemeği 1 kase yoğurt 1-2 dilim glutensiz ekmek Gece: 1 bardak süt + 5-6 adet çiğ badem");
            var denizUrunleri = await DietAddAsync("Deniz Ürünleri", "Deniz Ürünleri Sabah Kalkınca: 1 bardak ılık su KAHVALTI 1.Kahvaltı Menüsü 1 dilim beyaz peynir + 1 adet haşlanmış yumurta + 5-6  adet zeytin + Bol yeşillik + 1 dilim tam tahıllı ekmek 2. Kahvaltı Menüsü Avokadolu tost + 1 porsiyon meyve + Bol yeşillik ÖĞLE YEMEĞİ 1. Öğle Yemeği Menüsü Izgara balık + haşlama sebze + maden suyu 2. Öğle Yemeği Menüsü 1 porsiyon sebze yemeği +  1 kase yoğurt + 1 dilim ekmek AKŞAM YEMEĞİ 1. Akşam Yemeği Menüsü Kinoalı meyveli  salata 2. Akşam Yemeği Menüsü 1 porsiyon kurubaklagil yemeği + 3-4 yemek kaşığı bulgur pilavı + 1 kase salata (z.yağı ve limon ile) ARA ÖĞÜN ÖNERİLERİ 2 adet ceviz veya 5-6 adet badem/fındık 1 porsiyon meyve Bitki çayları Kahve çeşitleri");
            var yesilliklerDunyasi = await DietAddAsync("Yeşillikler Dünyası", "Yeşillikler Dünyası Kahvaltı: ! dilim diyet ekmeği, 2-3 dilim domates, 2-3 dilim salatalık, Şekersiz çay, Maydanoz, Marul. Ara Öğün: 2 tane kayısı, 1 tane elma. Öğlen Yemeği: Yeşillikli salata, 1 dilim diyet ekmeği, 1 kase çorba, 150 gram diyet yoğurt. Ara Öğün: 2 tane erik. Akşam Yemeği: 1 dilim diyet ekmeği, 1 kase çorba, Yeşillikli salata, 200 gram diyet yoğurt. Ara Öğün: 1 tane armut.");
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

            #region Yesillikler Dunyasi
            await DietFoodAddAsync(diet: yesilliklerDunyasi, diyetEkmek, domates, salatalık, cay,maydonoz,marul,
                elma, kayısı, kayısı, yesillikSalata, corba, erik,armut, diyetYogurt);
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