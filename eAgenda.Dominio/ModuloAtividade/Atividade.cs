using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using System;
using System.Collections.Generic;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : EntidadeBase<Atividade>
    {
        public Atividade()
        {
            Medicos = new List<Medico>();
        }

        public Atividade(DateTime d, TimeSpan hi, TimeSpan ht, bool f,TipoAtividade t,string a)
        {
            DataRealizacao = d;
            HoraInicio = hi;
            HoraTermino = ht;
            Finalizada = f;
            TipoAtividade = t;
            Assunto = a;
            Medicos = new List<Medico>();
            TempoDeDescanso = t.TempoDeDescanso;
        }

        public string Assunto { get; set; }
        public DateTime DataRealizacao { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public bool Finalizada { get; set; }
        public List<Medico> Medicos { get; set; }  
        private TipoAtividade TipoAtividade { get; set; }
        public TipoAtividadeEnum TipoAtividadeEnum { get; set; }
        public TimeSpan TempoDeDescanso { get; set; }

        public override void Atualizar(Atividade registro)
        {
            Id = registro.Id;
            DataRealizacao = registro.DataRealizacao;
            HoraInicio = registro.HoraInicio;
            HoraTermino = registro.HoraTermino;
            Finalizada = registro.Finalizada;
            TipoAtividadeEnum = registro.TipoAtividadeEnum;
            Assunto = registro.Assunto;
            Medicos = registro.Medicos;
        }

        public void AdicionarMedico(Medico medico)
        {
            try
            {
                medico.AdicionarAtividade(this);
                Medicos.Add(medico);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Atividade Clonar()
        {
            return MemberwiseClone() as Atividade;
        }

        public void AtribuirAtividade()
        {
            if(TipoAtividadeEnum == TipoAtividadeEnum.Cirurgia)
            {
                TipoAtividade = new Cirurgias();
                TempoDeDescanso = TipoAtividade.TempoDeDescanso;
            }
            else
            {
                TipoAtividade = new Consulta();
                TempoDeDescanso = TipoAtividade.TempoDeDescanso;
            }
        }


        public override bool Equals(object? obj)
        {
            return obj is Atividade atividade &&
                  Id == atividade.Id &&
                  DataRealizacao == atividade.DataRealizacao &&
                  HoraInicio == atividade.HoraInicio &&
                  HoraTermino == atividade.HoraTermino &&
                  Finalizada == atividade.Finalizada &&
                  TipoAtividadeEnum == atividade.TipoAtividadeEnum &&
                  Assunto == atividade.Assunto &&
                  Medicos == atividade.Medicos;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DataRealizacao, HoraInicio, HoraTermino, Finalizada, TipoAtividadeEnum, Assunto,Medicos);
        }

        public override string? ToString()
        {
            return " - " + Assunto + " - Data:  "+ DataRealizacao.ToString("d");
        }

        public bool VerificarSeAtividadeEstahFinalizada()
        {
            if(DataRealizacao.Date < DateTime.Now.Date || DataRealizacao.Date == DateTime.Now.Date && HoraTermino < DateTime.Now.TimeOfDay)
            {
                Finalizada = true;
                return true;
            }
            else
            {
                Finalizada = false;
                return false;
            }
        }
    }
}
