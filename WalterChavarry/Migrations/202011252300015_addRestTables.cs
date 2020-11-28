namespace WalterChavarry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRestTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Libros",
                c => new
                {
                    LibrosId = c.Int(nullable: false, identity: true),
                    Titulo = c.String(),
                    Autor = c.String(),
                    Dia = c.Int(nullable: false),
                    Mes = c.Int(nullable: false),
                    Anio = c.Int(nullable: false),
                    Idioma = c.String(),
                    PalabrasClave = c.String(),
                    Descripcion = c.String(),
                    Imagen_libro = c.Binary(),
                    Archivo = c.Binary(),
                })
                .PrimaryKey(t => t.LibrosId);

            CreateTable(
                "dbo.Comentarios",
                c => new
                {
                    ComentarioId = c.Int(nullable: false, identity: true),
                    Comentario_text = c.String(),
                    Usuario = c.String(),
                    Fecha = c.DateTime(nullable: false),
                    LibroId = c.Int(nullable: false),
                    Tipo = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ComentarioId);
        }
        
        public override void Down()
        {
        }
    }
}
