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
                        Curso_Id = c.Int(),
                        Instituicao_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.Curso_Id)
                .ForeignKey("dbo.InstituicaoEnsino", t => t.Instituicao_Id)
                .Index(t => t.Curso_Id)
                .Index(t => t.Instituicao_Id);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        InstituicaoEnsino_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstituicaoEnsino", t => t.InstituicaoEnsino_Id)
                .Index(t => t.InstituicaoEnsino_Id);
            
            CreateTable(
                "dbo.InstituicaoEnsino",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        Contrato_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contrato", t => t.Contrato_Id)
                .Index(t => t.Contrato_Id);
            
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
            DropForeignKey("dbo.Curso", "InstituicaoEnsino_Id", "dbo.InstituicaoEnsino");
            DropForeignKey("dbo.InstituicaoEnsino", "Contrato_Id", "dbo.Contrato");
            DropForeignKey("dbo.Aluno", "Instituicao_Id", "dbo.InstituicaoEnsino");
            DropForeignKey("dbo.Aluno", "Curso_Id", "dbo.Curso");
            DropIndex("dbo.InstituicaoEnsino", new[] { "Contrato_Id" });
            DropIndex("dbo.Curso", new[] { "InstituicaoEnsino_Id" });
            DropIndex("dbo.Aluno", new[] { "Instituicao_Id" });
            DropIndex("dbo.Aluno", new[] { "Curso_Id" });
            DropTable("dbo.Contrato");
            DropTable("dbo.InstituicaoEnsino");
            DropTable("dbo.Curso");
            DropTable("dbo.Aluno");
        }
    }
}
