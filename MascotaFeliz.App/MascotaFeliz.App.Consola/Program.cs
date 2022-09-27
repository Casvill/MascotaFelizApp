using System;
using MascotaFeliz.App.Dominio;
using MascotaFeliz.App.Persistencia;
using System.Collections.Generic;

namespace MascotaFeliz.App.Consola
{
    class Program
    {
        private static IRepositorioDueno _repoDueno = new RepositorioDueno(new Persistencia.AppContext());
        private static IRepositorioVeterinario _repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
        private static IRepositorioMascota _repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        private static IRepositorioHistoria _repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());
        private static IRepositorioVisitaPyP _repoVisitaPyP = new RepositorioVisitaPyP(new Persistencia.AppContext());

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //AddDueno();
            //AddVeterinario();
            //AddMascota();
            //AddHistoria();
            //AddVisitaPyP();
            //BuscarMascota(1);
            //BuscarMascotas();
            //AsignarVeterinario();
            //AsignarVisitaPyP();
        }

        private static void AddDueno()
        {
            var dueno = new Dueno
            {
                //Cedula = "1212",
                Nombres = "Juan",
                Apellidos = "Sin Miedo",
                Direccion = "Bajo un puente",
                Telefono = "1234567891",
                Correo = "juansinmiedo@gmail.com"
            };
            _repoDueno.AddDueno(dueno);
        }

        private static void AddVeterinario()
        {
            var veterinario = new Veterinario
            {
                //Cedula = "1212",
                Nombres = "Fulano",
                Apellidos = "Mengano",
                Direccion = "Estrella 23",
                Telefono = "15 313 85 77",
                //Correo = "fulgano@gmail.com"
                TarjetaProfesional = "Tarjeta123"
            };
            _repoVeterinario.AddVeterinario(veterinario);
            //Console.WriteLine("Right!");
        }

        private static void AddMascota()
        {
            var mascota = new Mascota
            {
                //Cedula = "1212",
                Nombre = "Firulais",
                Color = "Beige",
                Especie = "Perro",
                Raza = "Criollo"
            };
            _repoMascota.AddMascota(mascota);
            //Console.WriteLine("Right mascota!");
        }

        private static void AddHistoria()
        {
            var historia = new Historia
            {
                FechaInicial = DateTime.Today
            };
            _repoHistoria.AddHistoria(historia);
        }

        private static void AddVisitaPyP()
        {
            var visitaPyP = new VisitaPyP
            {
                //Cedula = "1212",
                FechaVisita = DateTime.Today,
                Temperatura = 35,
                Peso = 20,
                FrecuenciaRespiratoria = 50,
                FrecuenciaCardiaca = 200,
                EstadoAnimo = "Feliz",
                Recomendaciones = "Bañarlo"
            };
            _repoVisitaPyP.AddVisitaPyP(visitaPyP);
        }

        private static void BuscarMascota(int idMascota)
        {
            var mascota = _repoMascota.GetMascota(idMascota);
            Console.WriteLine("Nombre: " + mascota.Nombre + "\nColor: " + mascota.Color + "\nEspecie: " + mascota.Especie + "\nRaza: " + mascota.Raza + "\n");
        }

        private static void BuscarMascotas()
        {
            var mascotas = _repoMascota.GetAllMascotas();
            foreach (Mascota mascota in mascotas)
            {
                Console.WriteLine("Nombre: " + mascota.Nombre + "\nColor: " + mascota.Color + "\nEspecie: " + mascota.Especie + "\nRaza: " + mascota.Raza + "\n");
            }
        }

        private static void AsignarVeterinario()
        {
            var veterinario = _repoMascota.AsignarVeterinario(1, 15);
            Console.WriteLine(veterinario.Nombres+ " " + veterinario.Apellidos);
        }

       private static void AsignarVisitaPyP(int idHistoria)
        {
            var historia = _repoHistoria.GetHistoria(idHistoria);
            if (historia != null)
            {
                if (historia.VisitasPyP != null)
                {
                    historia.VisitasPyP.Add(new VisitaPyP { FechaVisita = new DateTime(2020, 01, 01), Temperatura = 38.0F, Peso = 30.0F, FrecuenciaRespiratoria = 71.0F, FrecuenciaCardiaca = 71.0F, EstadoAnimo = "Muy cansón", CedulaVeterinario = "123", Recomendaciones = "Dieta extrema"});
                }
                else
                {
                    historia.VisitasPyP = new List<VisitaPyP>{
                        new VisitaPyP{FechaVisita = new DateTime(2020, 01, 01), Temperatura = 38.0F, Peso = 30.0F, FrecuenciaRespiratoria = 71.0F, FrecuenciaCardiaca = 71.0F, EstadoAnimo = "Muy cansón", CedulaVeterinario = "123", Recomendaciones = "Dieta extrema" }
                    };
                }
                _repoHistoria.UpdateHistoria(historia);
            }
        }

    }
}
