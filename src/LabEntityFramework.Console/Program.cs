using LabEntityFrameworkBackEnd.Entidades;
using LabEntityFrameworkBackEnd.Repositorios;
using System;
using System.Collections.Generic;

namespace LabEntityFramework.ConsoleApp
{
    static class Program
    {
        private static Repositorio repositorio = new Repositorio();

        static readonly int quantidadeRegistrosExemploNMaisUm = 10000;
        static readonly int quantidadeRegistrosExemploProjecao = 500;
        static readonly int quantidadeRegistrosRecursosDesnecessarios = 1000;
        static readonly int quantidadeRegistrosObjetosDesnecessariosContexto = 20000;
        static readonly int quantidadeRegistrosBulkInsert = 1000;

        static void Main()
        {
            InicializaEF();

            MostrarMenu();
            SolicitarOpcaoMenu();

            Console.Read();
        }

        private static void InicializaEF()
        {
            Console.Write("Testando conexão com banco de dados... ");
            repositorio.InicializaEFMigrationsNaoAfetarTempo();
            Console.WriteLine("Conexão OK!");
        }

        static void Um_N_Mais_Problema()
        {
            Console.WriteLine(string.Format("Executando exemplo 1.1 com {0}, aguarde ...", quantidadeRegistrosExemploNMaisUm));
            var tempoExecucao = repositorio.Um_N_Mais_Problema(quantidadeRegistrosExemploNMaisUm);

            ImprimeResultadoRuim("com N + 1", tempoExecucao);
        }

        static void Um_N_Mais_Solucao()
        {
            Console.WriteLine(string.Format("Executando exemplo 1.2 com {0}, aguarde ...", quantidadeRegistrosExemploNMaisUm));
            var tempoExecucao = repositorio.Um_N_Mais_Solucao(quantidadeRegistrosExemploNMaisUm);

            ImprimeResultadoBom("com Include()", tempoExecucao);
        }


        static void Dois_Projecao_Sem_Projecao()
        {
            Console.WriteLine(string.Format("Executando exemplo 2.1 com {0}, aguarde ...", quantidadeRegistrosExemploProjecao));

            var tempoExecucao = repositorio.Dois_Projecao_Sem_Projecao(quantidadeRegistrosExemploProjecao);

            ImprimeResultadoRuim("SEM Projeção", tempoExecucao);
        }

        static void Dois_Projecao_Com_Projecao()
        {
            Console.WriteLine(string.Format("Executando exemplo 2.2 com {0}, aguarde ...", quantidadeRegistrosExemploProjecao));

            var tempoExecucao = repositorio.Dois_Projecao_Com_Projecao(quantidadeRegistrosExemploProjecao);

            ImprimeResultadoBom("COM Projeção", tempoExecucao);
        }

        static void Tres_Operacoes_Recursos_Desnecessarios()
        {
            Console.WriteLine(string.Format("Executando exemplo 3.1 com {0}, aguarde ...", quantidadeRegistrosRecursosDesnecessarios));

            var tempoExecucao = repositorio.Tres_UsoDetectChangesHabilitado(quantidadeRegistrosRecursosDesnecessarios);

            ImprimeResultadoRuim("com AutoDetectChanges HABILITADO", tempoExecucao);
        }

        static void Tres_Operacoes_Recursos_Desnecessarios_Otimizado()
        {
            Console.WriteLine(string.Format("Executando exemplo 3.2 com {0}, aguarde ...", quantidadeRegistrosRecursosDesnecessarios));

            var tempoExecucao = repositorio.Tres_UsoDetectChangesDesabilitado(quantidadeRegistrosRecursosDesnecessarios);

            ImprimeResultadoBom("AutoDetectChanges DESABILITADO", tempoExecucao);
        }


        static void Quatro_Objetos_Desnecessarios_Contexto()
        {
            Console.WriteLine(string.Format("Executando exemplo 4.1 com {0}, aguarde ...", quantidadeRegistrosObjetosDesnecessariosContexto));

            var tempoExecucao = repositorio.Quatro_Objetos_Desnecessarios_Contexto(quantidadeRegistrosObjetosDesnecessariosContexto);

            ImprimeResultadoRuim("SEM AsNoTracking()", tempoExecucao);
        }

        static void Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking()
        {
            Console.WriteLine(string.Format("Executando exemplo 4.2 com {0}, aguarde ...", quantidadeRegistrosObjetosDesnecessariosContexto));

            var tempoExecucao = repositorio.Quatro_Objetos_Desnecessarios_Contexto_Com_AsNoTracking(quantidadeRegistrosObjetosDesnecessariosContexto);

            ImprimeResultadoBom("COM AsNoTracking()", tempoExecucao);
        }

        static void InsertSemBulkInsert()
        {
            Console.WriteLine(string.Format("Executando exemplo 5.1 com {0} registros, aguarde ...", quantidadeRegistrosBulkInsert));

            var tempoExecucao = repositorio.InsertSemBulkInsert(quantidadeRegistrosBulkInsert);

            ImprimeResultadoRuim("SEM BulkInsert", tempoExecucao);
        }

        static void InsertComBulkInsert()
        {
            Console.WriteLine(string.Format("Executando exemplo 5.2 com {0} registros, aguarde ...", quantidadeRegistrosBulkInsert));

            var tempoExecucao = repositorio.InsertComBulkInsert(quantidadeRegistrosBulkInsert);

            ImprimeResultadoBom("COM BulkInsert", tempoExecucao);
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Exemplos N+1");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("1.1 - Executar consulta COM N+1.");
            Console.WriteLine("1.2 - Executar consulta SEM N+1.");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Exemplos uso de Projeção");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("2.1 - Executar consulta SEM Projeção.");
            Console.WriteLine("2.2 - Executar consulta COM Projeção.");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Exemplos recursos desnecessários");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("3.1 - Executar inserção com AutoDetectChanges HABILITADO.");
            Console.WriteLine("3.2 - Executar inserção com AutoDetectChanges DESABILITADO.");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Exemplos objetos desnecessários no contexto");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("4.1 - Executar consulta SEM AsNoTracking().");
            Console.WriteLine("4.2 - Executar consulta COM AsNoTracking().");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Uso de BulkInsert");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("5.1 - Executar insert de forma simples.");
            Console.WriteLine("5.2 - Executar insert com BulkInsert.");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();
        }

        private static void SolicitarOpcaoMenu()
        {
            Console.WriteLine("\nEscolha o número da opção desejada e pressione enter.");

            var novaOpcao = Console.ReadLine();
            ExecutarOpcaoMenu(novaOpcao);
        }

        private static void ExecutarOpcaoMenu(string opcaoSelecionada)
        {
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
                    break;
            }
            
            SolicitarOpcaoMenu();
        }

        private static void ImprimeResultadoRuim(string texto, TimeSpan tempoExecucao)
        {
            Console.Write("\nTempo de execução " + texto + ": ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(tempoExecucao.TotalSeconds);
            Console.ResetColor();
            Console.Write(" segundos\n");
        }

        private static void ImprimeResultadoBom(string texto, TimeSpan tempoExecucao)
        {
            Console.Write("\nTempo de execução " + texto + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(tempoExecucao.TotalSeconds);
            Console.ResetColor();
            Console.Write(" segundos\n");
        }
    }
}
