using LabEntityFrameworkBackEnd.Entidades;
using LabEntityFrameworkBackEnd.Repositorios;
using System;
using System.Collections.Generic;

namespace LabEntityFramework.Console
{
    class Program
    {
        private static Repositorio repositorio;
        static readonly int numeroRegistrosExemploNMaisUm = 10000;
        static readonly int numeroRegistrosExemploProjecao = 100;
        static readonly int numeroRegistrosRecursosDesnecessarios = 1000;
        static readonly int numeroRegistrosObjetosDesnecessariosContexto = 20000;
        static readonly int numeroRegistrosBulkInsert = 1000;

        static void Main(string[] args)
        {
            repositorio = new Repositorio();
            ShowLoading("Testando conexão com banco de dados...");
            repositorio.InicializaEFMigrationsNaoAfetarTempo();
            ShowLoading("Conexão OK!");

            MostrarMenu();
            SolicitarOpcaoMenu();

            System.Console.Read();
        }

        static void Um_N_Mais_Problema()
        {
            ShowLoading(string.Format("Executando exemplo 1.1 com {0}, aguarde ...", numeroRegistrosExemploNMaisUm));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();
            var tempoExecucao = repositorio.Um_N_Mais_Problema(numeroRegistrosExemploNMaisUm);

            System.Console.WriteLine(string.Format("Tempo de execução com N + 1: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        static void Um_N_Mais_Solucao()
        {
            ShowLoading(string.Format("Executando exemplo 1.2 com {0}, aguarde ...", numeroRegistrosExemploNMaisUm));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucaoSem = repositorio.Um_N_Mais_Solucao(numeroRegistrosExemploNMaisUm);

            System.Console.WriteLine(string.Format("Tempo de execução com Include(): {0} segundos", tempoExecucaoSem.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();

        }


        static void Dois_Projecao_Sem_Projecao()
        {
            ShowLoading(string.Format("Executando exemplo 2.1 com {0}, aguarde ...", numeroRegistrosExemploProjecao));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.Dois_Projecao_Sem_Projecao(numeroRegistrosExemploProjecao);

            System.Console.WriteLine(string.Format("Tempo de execução SEM Projeção: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        static void Dois_Projecao_Com_Projecao()
        {
            ShowLoading(string.Format("Executando exemplo 2.2 com {0}, aguarde ...", numeroRegistrosExemploProjecao));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.Dois_Projecao_Com_Projecao(numeroRegistrosExemploProjecao);

            System.Console.WriteLine(string.Format("Tempo de execução COM Projeção: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }


        static void Tres_Operacoes_Recursos_Desnecessarios()
        {
            ShowLoading(string.Format("Executando exemplo 3.1 com {0}, aguarde ...", numeroRegistrosRecursosDesnecessarios));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.Tres_UsoDetectChangesHabilitado(numeroRegistrosRecursosDesnecessarios);

            System.Console.WriteLine(string.Format("Tempo de execução com AutoDetectChanges HABILITADO: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        static void Tres_Operacoes_Recursos_Desnecessarios_Otimizado()
        {
            ShowLoading(string.Format("Executando exemplo 3.2 com {0}, aguarde ...", numeroRegistrosRecursosDesnecessarios));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.Tres_UsoDetectChangesDesabilitado(numeroRegistrosRecursosDesnecessarios);

            System.Console.WriteLine(string.Format("Tempo de execução com AutoDetectChanges DESABILITADO: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }


        static void Quatro_Objetos_Desnecessarios_Contexto()
        {
            ShowLoading(string.Format("Executando exemplo 4.1 com {0}, aguarde ...", numeroRegistrosObjetosDesnecessariosContexto));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.Quatro_Objetos_Desnecessarios_Contexto(numeroRegistrosObjetosDesnecessariosContexto);

            System.Console.WriteLine(string.Format("Tempo de execução SEM AsNoTracking(): {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        static void Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking()
        {
            ShowLoading(string.Format("Executando exemplo 4.2 com {0}, aguarde ...", numeroRegistrosObjetosDesnecessariosContexto));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking(numeroRegistrosObjetosDesnecessariosContexto);

            System.Console.WriteLine(string.Format("Tempo de execução COM AsNoTracking(): {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        static void InsertSemBulkInsert()
        {
            ShowLoading(string.Format("Executando exemplo 5.1 com {0} registros, aguarde ...", numeroRegistrosBulkInsert));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.InsertSemBulkInsert(numeroRegistrosBulkInsert);

            System.Console.WriteLine(string.Format("Tempo de execução SEM BulkInsert: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        static void InsertComBulkInsert()
        {
            ShowLoading(string.Format("Executando exemplo 5.2 com {0} registros, aguarde ...", numeroRegistrosBulkInsert));
            repositorio.InicializaEFMigrationsNaoAfetarTempo();

            var tempoExecucao = repositorio.InsertComBulkInsert(numeroRegistrosBulkInsert);

            System.Console.WriteLine(string.Format("Tempo de execução COM BulkInsert: {0} segundos", tempoExecucao.TotalSeconds));
            System.Console.WriteLine("");

            SolicitarOpcaoMenu();
        }

        private static void MostrarMenu()
        {
            System.Console.WriteLine("Opções:");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("Exemplos N+1");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("1.1 - Executar consulta COM N+1.");
            System.Console.WriteLine("1.2 - Executar consulta SEM N+1.");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("Exemplos uso de Projeção");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("2.1 - Executar consulta SEM Projeção.");
            System.Console.WriteLine("2.2 - Executar consulta COM Projeção.");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("Exemplos recursos desnecessários");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("3.1 - Executar inserção com AutoDetectChanges HABILITADO.");
            System.Console.WriteLine("3.2 - Executar inserção com AutoDetectChanges DESABILITADO.");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("Exemplos objetos desnecessários no contexto");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("4.1 - Executar consulta SEM AsNoTracking().");
            System.Console.WriteLine("4.2 - Executar consulta COM AsNoTracking().");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("Uso de BulkInsert");
            System.Console.WriteLine("------------------------------------------------------");
            System.Console.WriteLine("5.1 - Executar insert de forma simples.");
            System.Console.WriteLine("5.2 - Executar insert com BulkInsert.");
            System.Console.WriteLine("------------------------------------------------------");
        }

        private static void SolicitarOpcaoMenu()
        {
            System.Console.WriteLine("Escolha o número da opção desejada e pressione qualquer tecla.");
            System.Console.WriteLine("");
            System.Console.CursorVisible = true;

            var novaOpcao = System.Console.ReadLine().ToString();
            ExecutarOpcaoMenu(novaOpcao);
        }

        private static void ExecutarOpcaoMenu(string opcaoSelecionada)
        {
            var opcoesValidas = new List<string> { "1.1", "1.2", "2.1", "2.2", "3.1", "3.2", "4.1", "4.2", "5.1", "5.2" };

            if (!opcoesValidas.Contains(opcaoSelecionada.Trim()))
            {
                System.Console.WriteLine("Opção inválida, digite novamente o número da opção desejada.");

                SolicitarOpcaoMenu();
            }

            switch (opcaoSelecionada)
            {
                case "1.1":
                    Um_N_Mais_Problema();
                    break;
                case "1.2":
                    Um_N_Mais_Solucao();
                    break;
                case "2.1":
                    Dois_Projecao_Sem_Projecao();
                    break;
                case "2.2":
                    Dois_Projecao_Com_Projecao();
                    break;
                case "3.1":
                    Tres_Operacoes_Recursos_Desnecessarios();
                    break;
                case "3.2":
                    Tres_Operacoes_Recursos_Desnecessarios_Otimizado();
                    break;
                case "4.1":
                    Quatro_Objetos_Desnecessarios_Contexto();
                    break;
                case "4.2":
                    Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking();
                    break;
                case "5.1":
                    InsertSemBulkInsert();
                    break;
                case "5.2":
                    InsertComBulkInsert();
                    break;
                default:
                    Um_N_Mais_Problema();
                    break;
            }

        }

        private static void ShowLoading(string mensagem)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(mensagem);
        }

    }
}
