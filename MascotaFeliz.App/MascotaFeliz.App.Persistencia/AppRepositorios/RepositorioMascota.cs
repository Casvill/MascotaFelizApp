using System;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioMascota : IRepositorioMascota
    {
        /// <summary>
        /// Referencia al contexto de mascota
        /// </summary>
        private readonly AppContext _appContext;
        /// <summary>
        /// Metodo Constructor Utiiza 
        /// Inyeccion de dependencias para indicar el contexto a utilizar
        /// </summary>
        /// <param name="appContext"></param>//
        
        public RepositorioMascota(AppContext appContext)
        {
            _appContext = appContext;
        }

        public Mascota AddMascota(Mascota mascota)
        {
            var mascotaAdicionado = _appContext.Mascotas.Add(mascota);
            _appContext.SaveChanges();
            return mascotaAdicionado.Entity;
        }

        public void DeleteMascota(int idMascota)
        {
            var mascotaEncontrado = _appContext.Mascotas.FirstOrDefault(d => d.Id == idMascota);
            if (mascotaEncontrado == null)
                return;
            _appContext.Mascotas.Remove(mascotaEncontrado);
            _appContext.SaveChanges();
        }

       public IEnumerable<Mascota> GetAllMascotas()
        {
            return GetAllMascotas_();
        }

        public IEnumerable<Mascota> GetMascotasPorFiltro(string filtro)
        {
            var Mascotas = GetAllMascotas(); // Obtiene todos los saludos
            if (Mascotas != null)  //Si se tienen saludos
            {
                if (!String.IsNullOrEmpty(filtro)) // Si el filtro tiene algun valor
                {
                    Mascotas = Mascotas.Where(s => s.Nombre.Contains(filtro));
                }
            }
            return Mascotas;
        }

        public IEnumerable<Mascota> GetAllMascotas_()
        {
            return _appContext.Mascotas;
        }

       /* public Mascota GetMascota(int idMascota)
        {
            return _appContext.Mascotas.FirstOrDefault(d => d.Id == idMascota);
        }*/

         public Mascota GetMascota(int idMascota)
        {
            return _appContext.Mascotas.Include("Dueno").Include("Veterinario").Include("Historia").FirstOrDefault(d => d.Id == idMascota);
        }

        public Mascota UpdateMascota(Mascota mascota)
        {
            var mascotaEncontrado = _appContext.Mascotas.FirstOrDefault(d => d.Id == mascota.Id);
            if (mascotaEncontrado != null)
            {
                mascotaEncontrado.Nombre = mascota.Nombre;
                mascotaEncontrado.Color = mascota.Color;
                mascotaEncontrado.Especie = mascota.Especie;
                mascotaEncontrado.Raza = mascota.Raza;
                //mascotaEncontrado.Correo = mascota.Correo;
                _appContext.SaveChanges();
            }
            return mascotaEncontrado;
        }     

        public Veterinario AsignarVeterinario(int idMascota, int idVeterinario){
            var mascotaEncontrado = _appContext.Mascotas.FirstOrDefault(m=> m.Id == idMascota);
            if (mascotaEncontrado != null)
            {
                 var veterinarioEncontrado = _appContext.Veterinarios.FirstOrDefault(v=> v.Id == idVeterinario);
                    if (veterinarioEncontrado != null)
                         {
                            mascotaEncontrado.Veterinario = veterinarioEncontrado;
                            _appContext.SaveChanges();
                         }
                         return veterinarioEncontrado;
            }
            return null;
        }

        public Dueno AsignarDueno(int idMascota, int idDueno){
            var mascotaEncontrado = _appContext.Mascotas.FirstOrDefault(m=> m.Id == idMascota);
            if (mascotaEncontrado != null)
            {
                 var duenoEncontrado = _appContext.Duenos.FirstOrDefault(v=> v.Id == idDueno);
                    if (duenoEncontrado != null)
                         {
                            mascotaEncontrado.Dueno = duenoEncontrado;
                            _appContext.SaveChanges();
                         }
                         return duenoEncontrado;
            }
            return null;
        }

        public Historia AsignarHistoria(int idMascota, int idHistoria){
            var mascotaEncontrado = _appContext.Mascotas.FirstOrDefault(m=> m.Id == idMascota);
            if (mascotaEncontrado != null)
            {
                 var historiaEncontrado = _appContext.Historias.FirstOrDefault(v=> v.Id == idHistoria);
                    if (historiaEncontrado != null)
                         {
                            mascotaEncontrado.Historia = historiaEncontrado;
                            _appContext.SaveChanges();
                         }
                         return historiaEncontrado;
            }
            return null;
        }
    }
}