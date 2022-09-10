using System;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioVeterinario : IRepositorioVeterinario
    {
        /// <summary>
        /// Referencia al contexto de Veterinario
        /// </summary>
        private readonly AppContext _appContext;
        /// <summary>
        /// Metodo Constructor Utiiza 
        /// Inyeccion de dependencias para indicar el contexto a utilizar
        /// </summary>
        /// <param name="appContext"></param>//
        
        public RepositorioVeterinario(AppContext appContext)
        {
            _appContext = appContext;
        }

        public Veterinario AddVeterinario(Veterinario veterinario)
        {
            var veterinarioAdicionado = _appContext.Veterinarios.Add(veterinario);
            _appContext.SaveChanges();
            return veterinarioAdicionado.Entity;
        }

        public void DeleteVeterinario(int idVeterinario)
        {
            var veterinarioEncontrado = _appContext.Veterinarios.FirstOrDefault(d => d.Id == idVeterinario);
            if (veterinarioEncontrado == null)
                return;
            _appContext.Veterinarios.Remove(veterinarioEncontrado);
            _appContext.SaveChanges();
        }

       public IEnumerable<Veterinario> GetAllVeterinarios()
        {
            return GetAllVeterinarios_();
        }

        public IEnumerable<Veterinario> GetVeterinariosPorFiltro(string filtro)
        {
            var Veterinarios = GetAllVeterinarios(); // Obtiene todos los saludos
            if (Veterinarios != null)  //Si se tienen saludos
            {
                if (!String.IsNullOrEmpty(filtro)) // Si el filtro tiene algun valor
                {
                    Veterinarios = Veterinarios.Where(s => s.Nombres.Contains(filtro));
                }
            }
            return Veterinarios;
        }

        public IEnumerable<Veterinario> GetAllVeterinarios_()
        {
            return _appContext.Veterinarios;
        }

        public Veterinario GetVeterinario(int idVeterinario)
        {
            return _appContext.Veterinarios.FirstOrDefault(d => d.Id == idVeterinario);
        }

        public Veterinario UpdateVeterinario(Veterinario Veterinario)
        {
            var veterinarioEncontrado = _appContext.Veterinarios.FirstOrDefault(d => d.Id == Veterinario.Id);
            if (veterinarioEncontrado != null)
            {
                veterinarioEncontrado.Nombres = Veterinario.Nombres;
                veterinarioEncontrado.Apellidos = Veterinario.Apellidos;
                veterinarioEncontrado.Direccion = Veterinario.Direccion;
                veterinarioEncontrado.Telefono = Veterinario.Telefono;
                //veterinarioEncontrado.Correo = Veterinario.Correo;
                _appContext.SaveChanges();
            }
            return veterinarioEncontrado;
        }     
    }
}