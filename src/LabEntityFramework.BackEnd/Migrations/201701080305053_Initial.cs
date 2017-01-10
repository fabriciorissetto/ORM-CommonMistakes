namespace LabEntityFrameworkBackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Matricula = c.String(),
                        IdCurso = c.Int(),
                        IdInstituicao = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.IdCurso)
                .ForeignKey("dbo.InstituicaoEnsino", t => t.IdInstituicao)
                .Index(t => t.IdCurso)
                .Index(t => t.IdInstituicao);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        IdInstituicao = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstituicaoEnsino", t => t.IdInstituicao)
                .Index(t => t.IdInstituicao);
            
            CreateTable(
                "dbo.InstituicaoEnsino",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        IdContrato = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contrato", t => t.IdContrato)
                .Index(t => t.IdContrato);
            
            CreateTable(
                "dbo.Contrato",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroContrato = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aluno", "IdInstituicao", "dbo.InstituicaoEnsino");
            DropForeignKey("dbo.Curso", "IdInstituicao", "dbo.InstituicaoEnsino");
            DropForeignKey("dbo.InstituicaoEnsino", "IdContrato", "dbo.Contrato");
            DropForeignKey("dbo.Aluno", "IdCurso", "dbo.Curso");
            DropIndex("dbo.InstituicaoEnsino", new[] { "IdContrato" });
            DropIndex("dbo.Curso", new[] { "IdInstituicao" });
            DropIndex("dbo.Aluno", new[] { "IdInstituicao" });
            DropIndex("dbo.Aluno", new[] { "IdCurso" });
            DropTable("dbo.Contrato");
            DropTable("dbo.InstituicaoEnsino");
            DropTable("dbo.Curso");
            DropTable("dbo.Aluno");
        }
    }
}
