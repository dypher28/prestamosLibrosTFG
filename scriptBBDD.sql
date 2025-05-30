--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

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
-- Name: prestamoslibrostfg; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE prestamoslibrostfg WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';


ALTER DATABASE prestamoslibrostfg OWNER TO postgres;

\connect prestamoslibrostfg

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: alumnos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.alumnos (
    id_alumno integer NOT NULL,
    nombre character varying(100),
    apellidos character varying(100)
);


ALTER TABLE public.alumnos OWNER TO postgres;

--
-- Name: alumnos_id_alumno_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.alumnos_id_alumno_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.alumnos_id_alumno_seq OWNER TO postgres;

--
-- Name: alumnos_id_alumno_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.alumnos_id_alumno_seq OWNED BY public.alumnos.id_alumno;


--
-- Name: asignaturas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.asignaturas (
    id_asignatura integer NOT NULL,
    nombre character varying(100),
    curso_id integer
);


ALTER TABLE public.asignaturas OWNER TO postgres;

--
-- Name: asignaturas_id_asignatura_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.asignaturas_id_asignatura_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.asignaturas_id_asignatura_seq OWNER TO postgres;

--
-- Name: asignaturas_id_asignatura_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.asignaturas_id_asignatura_seq OWNED BY public.asignaturas.id_asignatura;


--
-- Name: cursos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.cursos (
    id_curso integer NOT NULL,
    nombre_curso character varying(100)
);


ALTER TABLE public.cursos OWNER TO postgres;

--
-- Name: cursos_id_curso_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.cursos_id_curso_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.cursos_id_curso_seq OWNER TO postgres;

--
-- Name: cursos_id_curso_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.cursos_id_curso_seq OWNED BY public.cursos.id_curso;


--
-- Name: images; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.images (
    id integer NOT NULL,
    file_name text,
    content_type text,
    data bytea
);


ALTER TABLE public.images OWNER TO postgres;

--
-- Name: images_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.images_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.images_id_seq OWNER TO postgres;

--
-- Name: images_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.images_id_seq OWNED BY public.images.id;


--
-- Name: libros; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.libros (
    id_libro integer NOT NULL,
    titulo character varying(100),
    editorial character varying(100),
    isbn character varying(20),
    asignatura_id integer,
    image integer,
    cantidad integer
);


ALTER TABLE public.libros OWNER TO postgres;

--
-- Name: libros_id_libro_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.libros_id_libro_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.libros_id_libro_seq OWNER TO postgres;

--
-- Name: libros_id_libro_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.libros_id_libro_seq OWNED BY public.libros.id_libro;


--
-- Name: matriculas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.matriculas (
    id_matricula integer NOT NULL,
    grupo character varying(100),
    alumno_id integer,
    curso_id integer,
    anio_escolar character varying(9)
);


ALTER TABLE public.matriculas OWNER TO postgres;

--
-- Name: matriculas_id_matricula_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.matriculas_id_matricula_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.matriculas_id_matricula_seq OWNER TO postgres;

--
-- Name: matriculas_id_matricula_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.matriculas_id_matricula_seq OWNED BY public.matriculas.id_matricula;


--
-- Name: paquetes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.paquetes (
    id_paquete integer NOT NULL,
    nombre character varying(255),
    curso_id integer
);


ALTER TABLE public.paquetes OWNER TO postgres;

--
-- Name: paquetes_id_paquete_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.paquetes_id_paquete_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.paquetes_id_paquete_seq OWNER TO postgres;

--
-- Name: paquetes_id_paquete_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.paquetes_id_paquete_seq OWNED BY public.paquetes.id_paquete;


--
-- Name: paquetes_libros; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.paquetes_libros (
    id_paquete integer NOT NULL,
    id_libro integer NOT NULL
);


ALTER TABLE public.paquetes_libros OWNER TO postgres;

--
-- Name: prestamos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.prestamos (
    id_prestamo integer NOT NULL,
    matricula_id integer,
    paquete_id integer,
    fecha_prestamo date DEFAULT CURRENT_DATE,
    fecha_devolucion date,
    devuelto boolean
);


ALTER TABLE public.prestamos OWNER TO postgres;

--
-- Name: prestamos_id_prestamo_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.prestamos_id_prestamo_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.prestamos_id_prestamo_seq OWNER TO postgres;

--
-- Name: prestamos_id_prestamo_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.prestamos_id_prestamo_seq OWNED BY public.prestamos.id_prestamo;


--
-- Name: alumnos id_alumno; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alumnos ALTER COLUMN id_alumno SET DEFAULT nextval('public.alumnos_id_alumno_seq'::regclass);


--
-- Name: asignaturas id_asignatura; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.asignaturas ALTER COLUMN id_asignatura SET DEFAULT nextval('public.asignaturas_id_asignatura_seq'::regclass);


--
-- Name: cursos id_curso; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cursos ALTER COLUMN id_curso SET DEFAULT nextval('public.cursos_id_curso_seq'::regclass);


--
-- Name: images id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.images ALTER COLUMN id SET DEFAULT nextval('public.images_id_seq'::regclass);


--
-- Name: libros id_libro; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.libros ALTER COLUMN id_libro SET DEFAULT nextval('public.libros_id_libro_seq'::regclass);


--
-- Name: matriculas id_matricula; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matriculas ALTER COLUMN id_matricula SET DEFAULT nextval('public.matriculas_id_matricula_seq'::regclass);


--
-- Name: paquetes id_paquete; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paquetes ALTER COLUMN id_paquete SET DEFAULT nextval('public.paquetes_id_paquete_seq'::regclass);


--
-- Name: prestamos id_prestamo; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prestamos ALTER COLUMN id_prestamo SET DEFAULT nextval('public.prestamos_id_prestamo_seq'::regclass);


--
-- Data for Name: alumnos; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.alumnos VALUES (1, 'Laura', 'Martínez Pérez');
INSERT INTO public.alumnos VALUES (2, 'Carlos', 'Gómez Ruiz');
INSERT INTO public.alumnos VALUES (3, 'Ana', 'López García');
INSERT INTO public.alumnos VALUES (4, 'David', 'Sánchez Torres');
INSERT INTO public.alumnos VALUES (5, 'María', 'Gómez Pérez');
INSERT INTO public.alumnos VALUES (6, 'Juan', 'López Rodríguez');
INSERT INTO public.alumnos VALUES (7, 'Lucía', 'Martínez García');
INSERT INTO public.alumnos VALUES (8, 'Carlos', 'Sánchez Fernández');
INSERT INTO public.alumnos VALUES (9, 'Ana', 'Ruiz López');
INSERT INTO public.alumnos VALUES (10, 'Pedro', 'Hernández Morales');
INSERT INTO public.alumnos VALUES (11, 'Laura', 'Jiménez Ortega');
INSERT INTO public.alumnos VALUES (12, 'Miguel', 'Díaz Torres');
INSERT INTO public.alumnos VALUES (13, 'Elena', 'Romero Castro');
INSERT INTO public.alumnos VALUES (14, 'David', 'Alonso Navarro');
INSERT INTO public.alumnos VALUES (15, 'Sofía', 'Vega Márquez');
INSERT INTO public.alumnos VALUES (16, 'Javier', 'Ramos Domínguez');
INSERT INTO public.alumnos VALUES (17, 'Carmen', 'Cano Suárez');
INSERT INTO public.alumnos VALUES (18, 'Alejandro', 'Gil Herrera');
INSERT INTO public.alumnos VALUES (19, 'Paula', 'Molina León');
INSERT INTO public.alumnos VALUES (20, 'Hugo', 'Iglesias Cruz');
INSERT INTO public.alumnos VALUES (21, 'Irene', 'Peña Vargas');
INSERT INTO public.alumnos VALUES (22, 'Andrés', 'Silva Méndez');
INSERT INTO public.alumnos VALUES (23, 'Sara', 'Ortega Robles');
INSERT INTO public.alumnos VALUES (24, 'Diego', 'Blanco Cortés');


--
-- Data for Name: asignaturas; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.asignaturas VALUES (1, 'Matemáticas', 1);
INSERT INTO public.asignaturas VALUES (2, 'Lengua Castellana', 1);
INSERT INTO public.asignaturas VALUES (5, 'Educación Física', 1);
INSERT INTO public.asignaturas VALUES (6, 'Matemáticas', 2);
INSERT INTO public.asignaturas VALUES (7, 'Lengua Castellana', 2);
INSERT INTO public.asignaturas VALUES (10, 'Educación Física', 2);
INSERT INTO public.asignaturas VALUES (11, 'Matemáticas', 3);
INSERT INTO public.asignaturas VALUES (12, 'Lengua Castellana', 3);
INSERT INTO public.asignaturas VALUES (13, 'Ciencias Sociales', 3);
INSERT INTO public.asignaturas VALUES (14, 'Ciencias Naturales', 3);
INSERT INTO public.asignaturas VALUES (15, 'Educación Física', 3);
INSERT INTO public.asignaturas VALUES (17, 'Lengua Castellana', 4);
INSERT INTO public.asignaturas VALUES (18, 'Ciencias Sociales', 4);
INSERT INTO public.asignaturas VALUES (19, 'Ciencias Naturales', 4);
INSERT INTO public.asignaturas VALUES (20, 'Educación Física', 4);
INSERT INTO public.asignaturas VALUES (21, 'Matemáticas', 5);
INSERT INTO public.asignaturas VALUES (22, 'Lengua Castellana', 5);
INSERT INTO public.asignaturas VALUES (23, 'Ciencias Sociales', 5);
INSERT INTO public.asignaturas VALUES (24, 'Ciencias Naturales', 5);
INSERT INTO public.asignaturas VALUES (25, 'Educación Física', 5);
INSERT INTO public.asignaturas VALUES (26, 'Matemáticas', 6);
INSERT INTO public.asignaturas VALUES (27, 'Lengua Castellana', 6);
INSERT INTO public.asignaturas VALUES (28, 'Ciencias Sociales', 6);
INSERT INTO public.asignaturas VALUES (29, 'Ciencias Naturales', 6);
INSERT INTO public.asignaturas VALUES (30, 'Educación Física', 6);
INSERT INTO public.asignaturas VALUES (3, 'Conocimiento del medio', 1);
INSERT INTO public.asignaturas VALUES (31, 'Religión', 1);
INSERT INTO public.asignaturas VALUES (32, 'Sociales', 1);
INSERT INTO public.asignaturas VALUES (16, 'Matematicas', 4);
INSERT INTO public.asignaturas VALUES (33, 'Música', 1);
INSERT INTO public.asignaturas VALUES (34, 'Plástica', 1);
INSERT INTO public.asignaturas VALUES (8, 'Música', 2);
INSERT INTO public.asignaturas VALUES (9, 'Conocimiento del medio', 2);
INSERT INTO public.asignaturas VALUES (35, 'Sociales', 2);
INSERT INTO public.asignaturas VALUES (36, 'Religión', 2);
INSERT INTO public.asignaturas VALUES (37, 'Plástica', 2);


--
-- Data for Name: cursos; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.cursos VALUES (4, '4º');
INSERT INTO public.cursos VALUES (3, '3º');
INSERT INTO public.cursos VALUES (1, '1º');
INSERT INTO public.cursos VALUES (5, '5º');
INSERT INTO public.cursos VALUES (6, '6º');
INSERT INTO public.cursos VALUES (2, '2º');


--
-- Data for Name: images; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.images VALUES (0, NULL, NULL, NULL);


--
-- Data for Name: libros; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.libros VALUES (27, 'CDSFDSF', 'GFDGFD', '4345556', 16, NULL, 4);
INSERT INTO public.libros VALUES (1, 'sdsd', 'asdsd', 'asdasd', 18, NULL, 1);
INSERT INTO public.libros VALUES (44, 'Lengua Castellana', 'Santillana', '4234234324', 22, NULL, 99);
INSERT INTO public.libros VALUES (28, 'JJ', 'YTRHT', '667657', 18, NULL, 5);
INSERT INTO public.libros VALUES (22, 'ASD', 'asdfasf', '123', 17, NULL, 1);
INSERT INTO public.libros VALUES (19, '11111111', '11111', '1111111111', 23, NULL, 1);
INSERT INTO public.libros VALUES (29, 'MATES FACILES', 'RFRE', '43555', 18, NULL, 3);
INSERT INTO public.libros VALUES (43, 'Javi El Dinosaurio', 'SM', '432432423423432', 22, NULL, 1);
INSERT INTO public.libros VALUES (30, 'mates dificiles', 'vfrgre', '665432', 18, NULL, 5);
INSERT INTO public.libros VALUES (42, 'asdf', 'asdf', '12345', 6, NULL, 1);
INSERT INTO public.libros VALUES (24, 'wwewe', 'sdsdsd', '1111111111111111', 8, NULL, 1);


--
-- Data for Name: matriculas; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.matriculas VALUES (2, 'A', 2, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (3, 'A', 3, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (4, 'B', 4, 2, '2024-2025');
INSERT INTO public.matriculas VALUES (6, 'C', 6, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (8, 'A', 8, 2, '2024-2025');
INSERT INTO public.matriculas VALUES (9, 'B', 9, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (11, 'A', 11, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (12, 'B', 12, 2, '2024-2025');
INSERT INTO public.matriculas VALUES (13, 'C', 13, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (14, 'A', 14, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (15, 'B', 15, 2, '2024-2025');
INSERT INTO public.matriculas VALUES (16, 'C', 16, 2, '2024-2025');
INSERT INTO public.matriculas VALUES (17, 'A', 17, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (19, 'C', 19, 2, '2024-2025');
INSERT INTO public.matriculas VALUES (20, 'A', 20, 1, '2024-2025');
INSERT INTO public.matriculas VALUES (7, 'C', 7, 4, '2024-2025');
INSERT INTO public.matriculas VALUES (18, 'B', 18, 6, '2024-2025');
INSERT INTO public.matriculas VALUES (5, 'B', 5, 3, '2024-2025');
INSERT INTO public.matriculas VALUES (10, 'C', 10, 5, '2024-2025');
INSERT INTO public.matriculas VALUES (1, 'A', 1, 5, '2024-2025');


--
-- Data for Name: paquetes; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.paquetes VALUES (18, 'paquete 2', 4);
INSERT INTO public.paquetes VALUES (19, 'paquete prueba', 2);
INSERT INTO public.paquetes VALUES (20, 'manolo2', 4);
INSERT INTO public.paquetes VALUES (21, 'blanca', 4);
INSERT INTO public.paquetes VALUES (22, 'ooo', 4);
INSERT INTO public.paquetes VALUES (23, 'Paquete 5º', 5);
INSERT INTO public.paquetes VALUES (24, 'Paquete bug', 5);


--
-- Data for Name: paquetes_libros; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.paquetes_libros VALUES (20, 22);
INSERT INTO public.paquetes_libros VALUES (20, 28);
INSERT INTO public.paquetes_libros VALUES (21, 27);
INSERT INTO public.paquetes_libros VALUES (21, 1);
INSERT INTO public.paquetes_libros VALUES (22, 22);
INSERT INTO public.paquetes_libros VALUES (22, 28);
INSERT INTO public.paquetes_libros VALUES (23, 43);
INSERT INTO public.paquetes_libros VALUES (23, 44);
INSERT INTO public.paquetes_libros VALUES (24, 44);
INSERT INTO public.paquetes_libros VALUES (24, 19);
INSERT INTO public.paquetes_libros VALUES (18, 22);
INSERT INTO public.paquetes_libros VALUES (18, 28);
INSERT INTO public.paquetes_libros VALUES (18, 29);
INSERT INTO public.paquetes_libros VALUES (18, 30);


--
-- Data for Name: prestamos; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.prestamos VALUES (15, 7, 20, '2025-05-29', NULL, false);
INSERT INTO public.prestamos VALUES (16, 8, 19, '2025-05-29', NULL, false);
INSERT INTO public.prestamos VALUES (17, 7, 19, '2025-05-29', NULL, false);
INSERT INTO public.prestamos VALUES (18, 7, 20, '2025-05-29', NULL, false);
INSERT INTO public.prestamos VALUES (19, 1, 23, '2025-05-29', NULL, false);
INSERT INTO public.prestamos VALUES (20, 10, 23, '2025-05-29', NULL, false);
INSERT INTO public.prestamos VALUES (21, 1, 24, '2025-05-29', NULL, false);


--
-- Name: alumnos_id_alumno_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.alumnos_id_alumno_seq', 24, true);


--
-- Name: asignaturas_id_asignatura_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.asignaturas_id_asignatura_seq', 37, true);


--
-- Name: cursos_id_curso_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.cursos_id_curso_seq', 6, true);


--
-- Name: images_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.images_id_seq', 32, true);


--
-- Name: libros_id_libro_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.libros_id_libro_seq', 44, true);


--
-- Name: matriculas_id_matricula_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.matriculas_id_matricula_seq', 20, true);


--
-- Name: paquetes_id_paquete_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.paquetes_id_paquete_seq', 24, true);


--
-- Name: prestamos_id_prestamo_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.prestamos_id_prestamo_seq', 21, true);


--
-- Name: alumnos alumnos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alumnos
    ADD CONSTRAINT alumnos_pkey PRIMARY KEY (id_alumno);


--
-- Name: asignaturas asignaturas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.asignaturas
    ADD CONSTRAINT asignaturas_pkey PRIMARY KEY (id_asignatura);


--
-- Name: cursos cursos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cursos
    ADD CONSTRAINT cursos_pkey PRIMARY KEY (id_curso);


--
-- Name: images images_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.images
    ADD CONSTRAINT images_pkey PRIMARY KEY (id);


--
-- Name: libros libros_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.libros
    ADD CONSTRAINT libros_pkey PRIMARY KEY (id_libro);


--
-- Name: matriculas matriculas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matriculas
    ADD CONSTRAINT matriculas_pkey PRIMARY KEY (id_matricula);


--
-- Name: paquetes_libros paquetes_libros_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paquetes_libros
    ADD CONSTRAINT paquetes_libros_pkey PRIMARY KEY (id_paquete, id_libro);


--
-- Name: paquetes paquetes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paquetes
    ADD CONSTRAINT paquetes_pkey PRIMARY KEY (id_paquete);


--
-- Name: prestamos prestamos_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prestamos
    ADD CONSTRAINT prestamos_pkey PRIMARY KEY (id_prestamo);


--
-- Name: asignaturas asignaturas_curso_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.asignaturas
    ADD CONSTRAINT asignaturas_curso_id_fkey FOREIGN KEY (curso_id) REFERENCES public.cursos(id_curso);


--
-- Name: libros libros_asignatura_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.libros
    ADD CONSTRAINT libros_asignatura_id_fkey FOREIGN KEY (asignatura_id) REFERENCES public.asignaturas(id_asignatura);


--
-- Name: libros libros_images_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.libros
    ADD CONSTRAINT libros_images_id_fk FOREIGN KEY (image) REFERENCES public.images(id);


--
-- Name: matriculas matriculas_alumno_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matriculas
    ADD CONSTRAINT matriculas_alumno_id_fkey FOREIGN KEY (alumno_id) REFERENCES public.alumnos(id_alumno);


--
-- Name: matriculas matriculas_curso_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.matriculas
    ADD CONSTRAINT matriculas_curso_id_fkey FOREIGN KEY (curso_id) REFERENCES public.cursos(id_curso);


--
-- Name: paquetes paquetes_curso_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paquetes
    ADD CONSTRAINT paquetes_curso_id_fkey FOREIGN KEY (curso_id) REFERENCES public.cursos(id_curso);


--
-- Name: paquetes_libros paquetes_libros_id_libro_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paquetes_libros
    ADD CONSTRAINT paquetes_libros_id_libro_fkey FOREIGN KEY (id_libro) REFERENCES public.libros(id_libro);


--
-- Name: paquetes_libros paquetes_libros_id_paquete_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.paquetes_libros
    ADD CONSTRAINT paquetes_libros_id_paquete_fkey FOREIGN KEY (id_paquete) REFERENCES public.paquetes(id_paquete);


--
-- Name: prestamos prestamos_matricula_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prestamos
    ADD CONSTRAINT prestamos_matricula_id_fkey FOREIGN KEY (matricula_id) REFERENCES public.matriculas(id_matricula);


--
-- Name: prestamos prestamos_paquete_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.prestamos
    ADD CONSTRAINT prestamos_paquete_id_fkey FOREIGN KEY (paquete_id) REFERENCES public.paquetes(id_paquete);


--
-- PostgreSQL database dump complete
--

