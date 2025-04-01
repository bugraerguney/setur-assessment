# 📦 Setur Mikroservis Projesi

Bu proje, **.NET 8** teknolojisi kullanılarak geliştirilmiş, mikroservis mimarisi barındıran bir yapıdır.  
Toplamda **2 adet mikroservis** ve **1 adet background worker servisi** içermektedir.  
Veri alışverişi için **RabbitMQ** kullanılmakta, veritabanları ise **PostgreSQL** üzerinde Docker ile çalıştırılmaktadır.

---

## 🧱 Proje Yapısı

| Servis                | Açıklama                                                  |
|------------------------|-----------------------------------------------------------|
| **Contact Microservice** | Kişi bilgileri ve iletişim bilgilerini yönetir            |
| **Report Microservice**  | Rapor taleplerini alır ve RabbitMQ ile kuyruğa gönderir   |
| **Worker Service**       | Kuyruktaki rapor mesajlarını dinler ve raporu oluşturur  |

---

## 🚀 Teknolojiler

- ✅ .NET 8  
- ✅ PostgreSQL (Docker üzerinden)  
- ✅ RabbitMQ (CloudAMQP üzerinden)  
- ✅ AutoMapper  
- ✅ FluentValidation  
- ✅ Entity Framework Core  
- ✅ BackgroundService (Hosted Service)
- ✅ xUnit
---

## 🔧 Dış Bağımlılıklar

### 🐇 RabbitMQ (CloudAMQP üzerinden)
amqps://yssvksxu:e114OLQHtG64zla_jGpEaU8WOdQ40L7t@leopard.lmq.cloudamqp.com/yssvksxu

#### 📁 ContactDb:
Username=postgres
Password=123456aA

####📁 ReportDb: 
Username=postgres
Password=123456aA*

🐳 Veritabanlarını Docker ile Kurma
Proje içinde paylaşılan Setur-Db-Contact-Docker ve Setur-Db-Report-Docker klasöründe aşağıdakiler yer alır:

Kurmak için:

cd Setur-Db-Contact-Docker
docker-compose up -d

Setur-Db-Report-Docker
docker-compose up -d

Veritabanları şu şekilde erişilebilir olur:

Veritabanı	Port	Kullanıcı	Şifre
ContactDb	1510	postgres	123456aA
ReportDb	1511	postgres	123456aA*

🏃‍♂️ Projeyi Çalıştırmak için
1. RabbitMQ CloudAMQP bağlantısı hazır olmalı (Cloud’da çalışıyor)
2. Docker ile veritabanlarını ayağa kaldır
bash
docker-compose up -d
3. Projeleri dotnet run ile çalıştırınız:
Contact Servisi:
bash
cd Setur.Contact.API
dotnet run

Report Servisi:
cd Setur.Report.API
dotnet run

Worker Servisi:
bash
cd Setur.ReportCreateWorkerService
dotnet run

NOT:http://localhost:7051/api/ReportContacts adresine post isteği attığınızda rapor oluşturma talebiniz başlamış olur.
