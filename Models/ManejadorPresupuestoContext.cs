namespace ManejadorPresupuesto2.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
     using System.Linq;
    using System.Collections.Generic;

    public class ManejadorPresupuestoContext: DbContext
    {
         public DbSet<Categoria> Categorias { get; set; }
         public DbSet<Estado> Estados { get; set; }
         public DbSet<ItemCategoria> ItemsCategoria { get; set; }
         public DbSet<EncabezadoMovimientoFinanciero> EncabezadoMovimientosFinanciero { get; set; }
         public DbSet<DetalleMovimientoIngreso> DetalleMovimientoIngresos { get; set; }
         public DbSet<DetalleMovimientoGasto> DetalleMovimientoGastos { get; set; }

        public ManejadorPresupuestoContext(DbContextOptions<ManejadorPresupuestoContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<Categoria> categorias = CargarCategorias();
            List<Estado> estados = CargarEstados();
            List<ItemCategoria> itemsCategoria = CargarItemsCategoria(categorias);

            modelBuilder.Entity<Categoria>().HasData(categorias.ToArray());
            modelBuilder.Entity<Estado>().HasData(estados.ToArray());
            modelBuilder.Entity<ItemCategoria>().HasData(itemsCategoria.ToArray());
            // var encabezado = CargarEncabezado(categorias, estados);
            // modelBuilder.Entity<EncabezadoMovimientoFinanciero>().HasData(encabezado);            
        }

        private EncabezadoMovimientoFinanciero CargarEncabezado(List<Categoria> categorias, List<Estado> estados)
        {
            return new EncabezadoMovimientoFinanciero(){EstadoId = estados.FirstOrDefault().Id, Titulo ="Loquesea", FechaMovimiento = DateTime.Now };
        }

        private List<ItemCategoria> CargarItemsCategoria(List<Categoria> categorias)
        {
            List<ItemCategoria> listaItemsCategoria = new List<ItemCategoria>();

            foreach (Categoria categoria in categorias)
            {
                switch (categoria.NombreCategoria)
                {
                    case "Alimentacion":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Comida"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Tiendas"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Rellenitos"},
                            }                            
                        );
                    break;
                    case "Bancos":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Banco BBVA"}                                
                            }                            
                        );
                    break;
                    case "Favores daniela":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Trasnferencia Dpc"}
                            }                            
                        );
                    break;
                    case "Ingresos":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Pago Salario"}
                            }                            
                        );
                    break;
                    case "Mascotas":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Gato"}
                            }                            
                        );
                    break;
                    case "Papeleria":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Impresiones"}
                            }                            
                        );
                    break;
                    case "Salud Personal":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Salud Mama"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Dentologo"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Cremas"},
                            }                            
                        );
                    break;
                    case "Servicio Privado":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Administracion Edificio"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Binance"}
                            }                            
                        );
                    break;
                    case "Servicios Publicos":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Celular"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Luz"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Agua"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Gas-4940929"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Internet tigo"},
                            }                            
                        );
                    break;
                    case "Streaming":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Netflix"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Disneyplus"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Hbo"},
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Amazon"}
                            }                            
                        );
                    break;
                    case "Tarjetas Credito":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Visa Rappy"}
                            }                            
                        );
                    break;
                    case "TaTransporte":
                        listaItemsCategoria.AddRange                        
                        (
                            new List<ItemCategoria>()
                            {
                                new ItemCategoria(){Id = Guid.NewGuid().ToString(), CategoriaId = categoria.Id, NombreItem = "Transporte"}
                            }                            
                        );
                    break;
                    
                }  
                                
            }

           
           
           return listaItemsCategoria;
        }

        private List<Estado> CargarEstados()
        {
            List<Estado> listaEstados = new List<Estado>()
           {
             new Estado(){Id = Guid.NewGuid().ToString(), NombreEstado = "Activo"},
             new Estado(){Id = Guid.NewGuid().ToString(), NombreEstado = "Inactivo"},             
           };
           
           return listaEstados;
        }

        private List<Categoria> CargarCategorias()
        {
           List<Categoria> listaCategorias = new List<Categoria>()
           {
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Bancos"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Streaming"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Servicios Publicos"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Mascotas"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Governamentales"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Transporte"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Salud Personal"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Alimentacion"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Servicio Privado"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Ingresos"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Tarjetas Credito"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Favores daniela"},
             new Categoria(){Id = Guid.NewGuid().ToString(), NombreCategoria = "Papeleria"}
           };
           
           return listaCategorias;
        }
    }
}