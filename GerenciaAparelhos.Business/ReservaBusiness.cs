using GerenciaAparelhos.Models.Sistema;
using GerenciaAparelhos.Repository;

namespace GerenciaAparelhos.Business
{
    public static class ReservaBusiness
    {
        #region "Propriedades Privadas"

        private static ReservaRepository _repository
        {
            get
            {
                return new ReservaRepository();
            }
        }

        #endregion

        #region "Métodos Públicos"

        public static void ListarReservas()
        {
            try
            {
                var lstReservas = _repository.ListarReservas();

                if (lstReservas.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-17} | {3,-17}", "ID", "APARELHO", "RESERVA", "DEVOLUÇÃO"));
                    Console.WriteLine("----------------------------------------------------------------------");
                    foreach (var reserva in lstReservas)
                    {
                        AparelhoModel mAparelho = AparelhoBusiness.ConsultarAparelho(reserva.IdAparelho);

                        Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-17} | {3,-17}", reserva.Id, mAparelho.Nome, reserva.Reserva.ToString("dd/MM/yyyy HH:mm"), !reserva.Devolucao.Equals(DateTime.MinValue) ? reserva.Devolucao.ToString("dd/MM/yyyy HH:mm") : "X"));
                    }
                    Console.WriteLine("----------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Nenhuma reserva cadastrada até o momento!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarReservasPorAparelho()
        {
            try
            {
                Console.WriteLine();
                AparelhoModel aparelho = new();

                while (true)
                {
                    try
                    {
                        Console.Write("Informa o ID do aparelho para consultar as reservas do mesmo: ");
                        int idAparelho = int.Parse(Console.ReadLine());

                        aparelho = AparelhoBusiness.ConsultarAparelho(idAparelho);

                        if (aparelho.Id <= 0)
                        {
                            throw new Exception();
                        }

                        break;
                    }
                    catch
                    {
                        Console.WriteLine("ID do aparelho inválido, tente novamente!");
                        continue;
                    }
                }

                var lstReservas = _repository.ListarReservaPorAparelho(aparelho.Id);

                if (lstReservas.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-17} | {3,-17}", "ID", "APARELHO", "RESERVA", "DEVOLUÇÃO"));
                    Console.WriteLine("----------------------------------------------------------------------");
                    foreach (var reserva in lstReservas)
                    {
                        AparelhoModel mAparelho = AparelhoBusiness.ConsultarAparelho(reserva.IdAparelho);

                        Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-17} | {3,-17}", reserva.Id, mAparelho.Nome, reserva.Reserva.ToString("dd/MM/yyyy HH:mm"), !reserva.Devolucao.Equals(DateTime.MinValue) ? reserva.Devolucao.ToString("dd/MM/yyyy HH:mm") : "X"));
                    }
                    Console.WriteLine("----------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Nenhuma reserva localizada para o aparelho informado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ReservarAparelho()
        {
            try
            {
                Console.WriteLine();
                AparelhoModel aparelho = new();
                ReservaModel reserva = new();
                List<ReservaModel> lstAparelhosReserva = new();
                reserva.Inclusao = DateTime.Now;
                reserva.Devolucao = DateTime.MinValue;                

                while (true)
                {
                    try
                    {                        
                        Console.Write("Informe o ID do aparelho que deseja reservar: ");
                        int idAparelho = int.Parse(Console.ReadLine());

                        aparelho = AparelhoBusiness.ConsultarAparelho(idAparelho);

                        if (aparelho.Id <= 0)
                        {
                            throw new Exception();
                        }

                        reserva.IdAparelho = aparelho.Id;
                        lstAparelhosReserva = _repository.ListarReservaPorAparelho(reserva.IdAparelho);

                        if(lstAparelhosReserva.Count() > 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("O aparelho escholhido já está reservado para essas datas:");
                            Console.WriteLine();
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-17} | {3,-17}", "ID", "APARELHO", "RESERVA", "DEVOLUÇÃO"));
                            Console.WriteLine("----------------------------------------------------------------------");
                            foreach (var reservaItem in lstAparelhosReserva)
                            {
                                AparelhoModel mAparelho = AparelhoBusiness.ConsultarAparelho(reservaItem.IdAparelho);

                                Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-17} | {3,-17}", reservaItem.Id, mAparelho.Nome, reservaItem.Reserva.ToString("dd/MM/yyyy HH:mm"), !reservaItem.Devolucao.Equals(DateTime.MinValue) ? reservaItem.Devolucao.ToString("dd/MM/yyyy") : "X"));
                            }
                            Console.WriteLine("----------------------------------------------------------------------");
                        }

                        break;
                    }
                    catch
                    {
                        Console.WriteLine("ID do aparelho inválido, tente novamente!");
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Informe a data e hora que deseja reservar o aparelho (Formato DD/MM/AAAA HH:mm): ");
                        DateTime dataHoraReserva = DateTime.Parse(Console.ReadLine());

                        if(dataHoraReserva.Equals(DateTime.MinValue) || dataHoraReserva.ToString("HH:mm").Equals(DateTime.MinValue.ToString("HH:mm")) || dataHoraReserva < DateTime.Now)
                        {
                            throw new Exception("Formato de data e hora incorretos ou data menor que a atual, favor tentar novamente!");
                        }
                        else if(lstAparelhosReserva.Where(p => dataHoraReserva > p.Reserva && !p.Devolucao.Equals(DateTime.MinValue)).Count() < 0)
                        {
                            throw new Exception("O aparelho já possuí reserva para esta faixa de horário com intervalor de 1 hora de acrèscimo, favor escolher outro horário ou data!");
                        }

                        reserva.Reserva = dataHoraReserva;

                        break;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {                     
                        var lstProfessores = ProfessorBusiness.ListarProfessores();

                        if (lstProfessores.Count() > 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Professores cadastrados:");
                            Console.WriteLine();
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-16} ", "ID", "NOME", "CPF"));
                            Console.WriteLine("----------------------------------------------------------------------");
                            foreach (var professor in lstProfessores)
                            {
                                Console.WriteLine(String.Format("{0,6} | {1,-20} | {2,-16}", professor.Id, professor.Nome, professor.CPF));
                            }
                            Console.WriteLine("----------------------------------------------------------------------");                           
                        }

                        Console.WriteLine();
                        Console.Write("Informe o ID do professor que deseja reservar o aparelho: ");
                        int idProfessor = int.Parse(Console.ReadLine());

                        if (lstProfessores.Where(p => p.Id.Equals(idProfessor)).Count() <= 0)
                        {
                            throw new Exception();
                        }

                        reserva.IdProfessor = idProfessor;

                        break;
                    }
                    catch
                    {
                        Console.WriteLine("ID do professor inválido, tente novamente!");
                        continue;
                    }
                }

                bool bReservaCadastrada = _repository.CadastrarReserva(reserva);

                if (bReservaCadastrada)
                {
                    Console.WriteLine();
                    Console.WriteLine("Reserva cadastrada com sucesso!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Não foi possível cadastrar a reserva, favor tentar novamente!");
                }
            }
            catch
            {
                throw;
            }
        }

        public static void LiberarReserva()
        {
            try
            {
                Console.WriteLine();

                while (true)
                {
                    try
                    {
                        Console.Write("Essas são as reservas cadastradas: ");
                        Console.WriteLine();

                        ListarReservas();

                        Console.WriteLine();
                        Console.Write("Informe o ID da reserva que deseja liberar: ");
                        int idReserva = int.Parse(Console.ReadLine());

                        if(_repository.ListarReservas().Where(p => p.Id.Equals(idReserva)).Count() <= 0)
                        {
                            throw new Exception();
                        }

                        bool bLiberarReserva = _repository.LiberarReserva(idReserva);

                        if(bLiberarReserva)
                        {
                            Console.WriteLine("Reserva liberada com sucesso!");
                            break;
                        }
                        {
                            Console.WriteLine("Naõ foi possível liberar a reserva, tente novamente!");
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("ID da reserva inválido, tente novamente!");
                        continue;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
