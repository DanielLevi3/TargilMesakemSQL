--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: sp_get_all_workers_and_their_roles(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_all_workers_and_their_roles() RETURNS TABLE(id bigint, name text, salary integer, phone text, name_role text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
                    SELECT workers.id, workers.name,workers.salary,workers.phone, r.name FROM workers
                    join roles r on workers.roles_id = r.id
        order by workers.id;
    END;
    $$;


ALTER FUNCTION public.sp_get_all_workers_and_their_roles() OWNER TO postgres;

--
-- Name: sp_get_all_workers_in_same_site(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_all_workers_in_same_site(_sites_id integer) RETURNS TABLE(id bigint, name text, salary integer, phone text, name_site text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
            SELECT w.id, w.name,w.salary,w.phone, s.name FROM workers w
            join sites s on w.sites_id=s.id
        where s.id=_sites_id;
    END;
    $$;


ALTER FUNCTION public.sp_get_all_workers_in_same_site(_sites_id integer) OWNER TO postgres;

--
-- Name: sp_get_max_workers_in_same_site(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_max_workers_in_same_site() RETURNS TABLE(name_of_max_workers_in_site text, max_workers_in_site bigint)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
          select s.name,max(c.count2) max2 from
(select workers.sites_id,count(workers.sites_id)count2 from workers group by workers.sites_id)c
join sites s on c.sites_id=s.id
group by s.name
order by max2 desc
limit 1;

    END;
    $$;


ALTER FUNCTION public.sp_get_max_workers_in_same_site() OWNER TO postgres;

--
-- Name: sp_update_salary_bonus(); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_update_salary_bonus()
    LANGUAGE plpgsql
    AS $$
    declare
        x int= 500;
      BEGIN
        FOR i IN 1..(select max(workers.id) from workers)
            loop
            update workers set salary =x+salary
            where id=i;
            end loop;
    END;
    $$;


ALTER PROCEDURE public.sp_update_salary_bonus() OWNER TO postgres;

--
-- Name: sp_update_workers_salary(bigint); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_update_workers_salary(x bigint)
    LANGUAGE plpgsql
    AS $$
    begin

        IF(x=1)
        THEN
              update workers set salary= 20000
               where workers.roles_id=x;
          Else
            update workers set salary= (random()*5000)::int+5000
            where roles_id>=x;
            END IF;
          END;
    $$;


ALTER PROCEDURE public.sp_update_workers_salary(x bigint) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.roles (
    id bigint NOT NULL,
    name text
);


ALTER TABLE public.roles OWNER TO postgres;

--
-- Name: roles_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.roles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.roles_id_seq OWNER TO postgres;

--
-- Name: roles_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.roles_id_seq OWNED BY public.roles.id;


--
-- Name: sites; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sites (
    id bigint NOT NULL,
    name text NOT NULL,
    address text NOT NULL
);


ALTER TABLE public.sites OWNER TO postgres;

--
-- Name: sites_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sites_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sites_id_seq OWNER TO postgres;

--
-- Name: sites_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sites_id_seq OWNED BY public.sites.id;


--
-- Name: workers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.workers (
    id bigint NOT NULL,
    name text NOT NULL,
    phone text NOT NULL,
    salary integer,
    roles_id bigint NOT NULL,
    sites_id bigint NOT NULL
);


ALTER TABLE public.workers OWNER TO postgres;

--
-- Name: workers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.workers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.workers_id_seq OWNER TO postgres;

--
-- Name: workers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.workers_id_seq OWNED BY public.workers.id;


--
-- Name: roles id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles ALTER COLUMN id SET DEFAULT nextval('public.roles_id_seq'::regclass);


--
-- Name: sites id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sites ALTER COLUMN id SET DEFAULT nextval('public.sites_id_seq'::regclass);


--
-- Name: workers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workers ALTER COLUMN id SET DEFAULT nextval('public.workers_id_seq'::regclass);


--
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.roles (id, name) FROM stdin;
1	Engineer
2	Contractor
3	Project_Manager
4	Architect
5	Construction worker
\.


--
-- Data for Name: sites; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sites (id, name, address) FROM stdin;
1	Azrieli	Tel-Aviv
2	Empier_State	Manhattan
3	Mall_G	Yokneham
4	Osem	Modiein
5	Grand_Mall	Haifa
\.


--
-- Data for Name: workers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.workers (id, name, phone, salary, roles_id, sites_id) FROM stdin;
1	Danny	0542315764	20500	1	1
2	Gil	0542318764	6296	2	1
3	Yoav	0541218764	9177	3	1
4	Gal	0541218864	10201	4	1
5	Gal	05431002164	9288	5	1
6	Shimon	05431002164	5610	5	2
7	Haim	05431001464	9383	4	2
8	Nisim	05431001534	5846	3	2
9	Yohay	05431001534	8642	3	3
10	Jeffry	05031081534	8025	2	3
11	Joshoua	05031081909	20500	1	3
12	Kobi	05831081909	20500	1	4
13	Yossi	05315481909	10229	2	4
14	Avi	05318481909	6963	3	4
15	Oded	05518487909	7997	3	5
16	Menashe	0575647909	8259	5	5
17	Vladimir	0574247909	20500	1	5
18	Bob	0574247909	9870	2	5
19	Nir	0524246409	9904	4	5
20	Tzvi	0524211409	10033	3	5
\.


--
-- Name: roles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.roles_id_seq', 5, true);


--
-- Name: sites_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sites_id_seq', 5, true);


--
-- Name: workers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.workers_id_seq', 20, true);


--
-- Name: roles roles_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pk PRIMARY KEY (id);


--
-- Name: sites sites_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sites
    ADD CONSTRAINT sites_pk PRIMARY KEY (id);


--
-- Name: workers workers_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_pk PRIMARY KEY (id);


--
-- Name: workers workers_roles_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_roles_id_fk FOREIGN KEY (roles_id) REFERENCES public.roles(id);


--
-- Name: workers workers_sites_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workers
    ADD CONSTRAINT workers_sites_id_fk FOREIGN KEY (sites_id) REFERENCES public.sites(id);


--
-- PostgreSQL database dump complete
--

