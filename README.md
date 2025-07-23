# Personal Finance Tracker

Bu loyiha foydalanuvchilarga moliyaviy operatsiyalarni boshqarish, statistik ma'lumotlarni ko'rish va ma'lumotlarni eksport qilish imkonini beruvchi zamonaviy backend tizimidir.

## Texnologiyalar

- ** ASP.NET Core Web API** – Backend RESTful API uchun
- ** .NET Worker Service** – RabbitMQ navbatidan kelgan xabarlarni qayta ishlash uchun
- ** PostgreSQL** – Ma’lumotlar ombori
- ** Redis** – Kesh va tezkor ma’lumotlar uchun
- ** RabbitMQ** – Xabarlar navbati (message queue)
- ** Docker va Docker Compose** – Loyihani konteynerda boshqarish

---

## Loyiha tuzilmasi

Personal Finance Tracker
	-- docker-compose.yml
	-- src
		-- Application
		-- Domain
		-- Infrastructura
		-- WebApi
		-- Workers
	-- tests
		-- Application.Tests
		-- Infrastructure.Tests
		-- Integration.Tests

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Docker Compose orqali ishga tushirish

 Loyiha konteyner asosida bir nechta xizmatlardan tashkil topgan. Quyidagi buyruq orqali barchasini ishga tushurishingiz mumkin:

```bash```
docker-compose up --build

Loyihani swaggerda ushbu url orqali korishingiz mumkin

http://localhost:5000/index.html

RabbitM ning ui qismini ko'rish uchun quyidagi url

http://localhost:15672/

Apilar haqida qolgan barcha malumotlar swaggerda mavjud, har bir api qandya malumot olib kelishi va qanday malumot qaytarishi, apining vazifalari qisqacha yozilgan.