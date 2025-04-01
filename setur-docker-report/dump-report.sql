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
-- Name: ReportContacts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReportContacts" (
    "Id" uuid NOT NULL,
    "RequestedAt" timestamp with time zone NOT NULL,
    "CompletedAt" timestamp with time zone,
    "Status" integer NOT NULL
);


ALTER TABLE public."ReportContacts" OWNER TO postgres;

--
-- Name: ReportDetails; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReportDetails" (
    "Id" uuid NOT NULL,
    "ReportId" uuid NOT NULL,
    "Location" text NOT NULL,
    "PersonCount" integer NOT NULL,
    "PhoneNumberCount" integer NOT NULL
);


ALTER TABLE public."ReportDetails" OWNER TO postgres;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Data for Name: ReportContacts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ReportContacts" ("Id", "RequestedAt", "CompletedAt", "Status") FROM stdin;
5090e5ca-995b-4716-b94a-9f2a3270e357	2025-03-29 21:29:32.181373+00	\N	0
3087708f-5ff7-41a4-a79b-a83daa1b05de	2025-03-29 21:53:27.260965+00	\N	0
6c512a6c-4493-493b-bba8-a2f166212412	2025-03-29 22:09:52.989656+00	\N	0
db85b300-39fa-4ad5-8f3f-7fcf4259de4b	2025-03-29 22:11:58.95355+00	\N	0
887830d0-000b-44ba-9c6a-28a6a7e044f2	2025-03-29 22:15:47.290293+00	2025-03-29 22:15:52.074018+00	1
\.


--
-- Data for Name: ReportDetails; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ReportDetails" ("Id", "ReportId", "Location", "PersonCount", "PhoneNumberCount") FROM stdin;
853c2f99-db17-4c93-a2e4-d42d81622a12	887830d0-000b-44ba-9c6a-28a6a7e044f2	Adana	1	1
9a2fbdd6-991c-4964-a9d5-cbcfadd7093b	887830d0-000b-44ba-9c6a-28a6a7e044f2	İstanbul	2	1
c7a55fcf-7fb7-4d46-b76c-ff1ba4da16d6	887830d0-000b-44ba-9c6a-28a6a7e044f2	Ankara	1	1
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250328165655_initial_reportdb	8.0.14
\.


--
-- Name: ReportContacts PK_ReportContacts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReportContacts"
    ADD CONSTRAINT "PK_ReportContacts" PRIMARY KEY ("Id");


--
-- Name: ReportDetails PK_ReportDetails; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReportDetails"
    ADD CONSTRAINT "PK_ReportDetails" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_ReportDetails_ReportId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_ReportDetails_ReportId" ON public."ReportDetails" USING btree ("ReportId");


--
-- Name: ReportDetails FK_ReportDetails_ReportContacts_ReportId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ReportDetails"
    ADD CONSTRAINT "FK_ReportDetails_ReportContacts_ReportId" FOREIGN KEY ("ReportId") REFERENCES public."ReportContacts"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

