--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4 (Debian 17.4-1.pgdg120+2)
-- Dumped by pg_dump version 17.4 (Debian 17.4-1.pgdg120+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: ContactInfos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ContactInfos" (
    "Id" uuid NOT NULL,
    "PersonInfoId" uuid NOT NULL,
    "InfoType" integer NOT NULL,
    "Content" character varying(64) NOT NULL,
    "Created" timestamp with time zone NOT NULL,
    "Updated" timestamp with time zone
);


ALTER TABLE public."ContactInfos" OWNER TO postgres;

--
-- Name: PersonInfos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PersonInfos" (
    "Id" uuid NOT NULL,
    "Name" character varying(64) NOT NULL,
    "Surname" character varying(64) NOT NULL,
    "Company" character varying(64),
    "Created" timestamp with time zone NOT NULL,
    "Updated" timestamp with time zone
);


ALTER TABLE public."PersonInfos" OWNER TO postgres;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Data for Name: ContactInfos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ContactInfos" ("Id", "PersonInfoId", "InfoType", "Content", "Created", "Updated") FROM stdin;
5cebed9e-d6d9-44ab-8673-26a6f7a13d7b	5ce32cfe-455b-4fd2-8aee-c2624069f24c	3	İstanbul	2025-03-28 00:46:00.470556+00	\N
d13321fd-0c75-4a61-8642-bbac54e90080	1ac502d5-0aeb-42f7-abaf-4a4632c783fe	3	İstanbul	2025-03-29 01:03:40.635739+00	\N
bf7a5775-0df8-482d-97a1-3541f8a43b19	1ac502d5-0aeb-42f7-abaf-4a4632c783fe	1	+908888888888	2025-03-29 01:03:53.553349+00	\N
0df5f1a0-fc87-4910-a5f1-a20c2c2c1e3c	db085711-3b1e-41fb-a6af-0c94284ea76c	1	+908888888888	2025-03-29 01:04:10.850022+00	\N
967aa418-9942-47dc-ba95-dde29d918da7	db085711-3b1e-41fb-a6af-0c94284ea76c	3	Adana	2025-03-29 01:04:21.409912+00	\N
2b7baed8-0fa2-4c96-bc23-1fae876d162b	8d20ca68-f207-4ba8-9075-8f7d6d4e8781	3	Ankara	2025-03-29 01:04:35.761758+00	\N
2eb16b8e-b2eb-4362-bc24-60ee70292525	8d20ca68-f207-4ba8-9075-8f7d6d4e8781	1	+909999999999	2025-03-29 01:04:49.858078+00	\N
\.


--
-- Data for Name: PersonInfos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PersonInfos" ("Id", "Name", "Surname", "Company", "Created", "Updated") FROM stdin;
1ac502d5-0aeb-42f7-abaf-4a4632c783fe	Ali	Yıldız	Siemens	2025-03-29 00:58:09.331122+00	\N
db085711-3b1e-41fb-a6af-0c94284ea76c	Meltem	Şahin	Beko	2025-03-29 00:58:28.985839+00	\N
8d20ca68-f207-4ba8-9075-8f7d6d4e8781	Arzu	Toprak	Samsung	2025-03-29 00:58:47.661313+00	\N
5ce32cfe-455b-4fd2-8aee-c2624069f24c	Ahmet	Sancak	Samsung	2025-03-28 00:41:17.757323+00	2025-03-29 00:59:19.662493+00
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250327153821_initial	8.0.14
\.


--
-- Name: ContactInfos PK_ContactInfos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ContactInfos"
    ADD CONSTRAINT "PK_ContactInfos" PRIMARY KEY ("Id");


--
-- Name: PersonInfos PK_PersonInfos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PersonInfos"
    ADD CONSTRAINT "PK_PersonInfos" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_ContactInfos_PersonInfoId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ContactInfos_PersonInfoId" ON public."ContactInfos" USING btree ("PersonInfoId");


--
-- Name: ContactInfos FK_ContactInfos_PersonInfos_PersonInfoId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ContactInfos"
    ADD CONSTRAINT "FK_ContactInfos_PersonInfos_PersonInfoId" FOREIGN KEY ("PersonInfoId") REFERENCES public."PersonInfos"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

