using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CVP.Routines.MotorArquivosComunicacao.Console.Services
{
    public partial class ImportFilePrevConverterService
    {
        private static readonly Dictionary<string, List<FieldDefinition>> Layouts = new Dictionary<string, List<FieldDefinition>>
        {
            {
                "00", new List<FieldDefinition>
                {
                    new FieldDefinition("HEADER_TP_REGISTRO", 1, 2),
                    new FieldDefinition("HEADER_DT_REFERENCIA", 3, 10),
                    new FieldDefinition("HEADER_DT_GERACAO", 13, 26),
                    new FieldDefinition("HEADER_DT_EXTENSO", 39, 50),
                    new FieldDefinition("HEADER_NM_ARQUIVO", 89, 30),
                    new FieldDefinition("HEADER_TP_ENVIO", 119, 20),
                    new FieldDefinition("HEADER_filler", 139, 452),
                    new FieldDefinition("HEADER_SQ_ARQUIVO", 591, 10)
                }
            },

            {
                "01", new List<FieldDefinition>
                {
                    new FieldDefinition("SEGURADORA_TP_REGISTRO", 1, 2),
                    new FieldDefinition("SEGURADORA_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("SEGURADORA_CD_CIA", 18, 2),
                    new FieldDefinition("SEGURADORA_NM_CIA", 20, 40),
                    new FieldDefinition("SEGURADORA_NO_CNPJ", 60, 20),
                    new FieldDefinition("SEGURADORA_DS_LOGRADOURO", 80, 50),
                    new FieldDefinition("SEGURADORA_NO_LOGRADOURO", 130, 10),
                    new FieldDefinition("SEGURADORA_DS_COMPLEMENTO", 140, 15),
                    new FieldDefinition("SEGURADORA_DS_BAIRRO", 155, 30),
                    new FieldDefinition("SEGURADORA_DS_CIDADE", 185, 35),
                    new FieldDefinition("SEGURADORA_SG_UF", 220, 2),
                    new FieldDefinition("SEGURADORA_NO_CEP", 222, 10),
                    new FieldDefinition("SEGURADORA_filler", 232, 359),
                    new FieldDefinition("SEGURADORA_SQ_ARQUIVO", 591, 10)
                }
            },

            {
                "02", new List<FieldDefinition>
                {
                    new FieldDefinition("INSTITUIDORA_TP_REGISTRO", 1, 2),
                    new FieldDefinition("INSTITUIDORA_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("INSTITUIDORA_CD_INSTITUIDORA", 18, 15),
                    new FieldDefinition("INSTITUIDORA_NM_INSTITUIDORA", 33, 40),
                    new FieldDefinition("INSTITUIDORA_DS_LOGRADOURO", 73, 50),
                    new FieldDefinition("INSTITUIDORA_NO_LOGRADOURO", 123, 10),
                    new FieldDefinition("INSTITUIDORA_DS_COMPLEMENTO", 133, 15),
                    new FieldDefinition("INSTITUIDORA_DS_BAIRRO", 148, 30),
                    new FieldDefinition("INSTITUIDORA_DS_CIDADE", 178, 35),
                    new FieldDefinition("INSTITUIDORA_SG_UF", 213, 2),
                    new FieldDefinition("INSTITUIDORA_NO_CEP", 215, 10),
                    new FieldDefinition("INSTITUIDORA_NO_CNPJ", 225, 20),
                    new FieldDefinition("INSTITUIDORA_E_MAIL_EMPRESA", 245, 71),
                    new FieldDefinition("INSTITUIDORA_NM_CONTATO_EMPRESA", 316, 40),
                    new FieldDefinition("INSTITUIDORA_FORMA_CUSTEIO", 356, 60),
                    new FieldDefinition("INSTITUIDORA_filler", 416, 175),
                    new FieldDefinition("INSTITUIDORA_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "03", new List<FieldDefinition>
                {
                    new FieldDefinition("PARTICIPANTE_TP_REGISTRO", 1, 2),
                    new FieldDefinition("PARTICIPANTE_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("PARTICIPANTE_CD_BARRAS", 18, 17),
                    new FieldDefinition("PARTICIPANTE_CD_FORMULARIO", 35, 10),
                    new FieldDefinition("PARTICIPANTE_CD_CENTRO_CUSTO", 45, 20),
                    new FieldDefinition("PARTICIPANTE_NM_PRIMEIRO_NOME", 65, 20),
                    new FieldDefinition("PARTICIPANTE_NM_CLIENTE", 85, 65),
                    new FieldDefinition("PARTICIPANTE_DS_LOGRADOURO", 150, 50),
                    new FieldDefinition("PARTICIPANTE_NO_LOGRADOURO", 200, 10),
                    new FieldDefinition("PARTICIPANTE_DS_COMPLEMENTO", 210, 15),
                    new FieldDefinition("PARTICIPANTE_DS_BAIRRO", 225, 30),
                    new FieldDefinition("PARTICIPANTE_DS_CIDADE", 255, 35),
                    new FieldDefinition("PARTICIPANTE_SG_UF", 290, 2),
                    new FieldDefinition("PARTICIPANTE_NO_CEP", 292, 10),
                    new FieldDefinition("PARTICIPANTE_NO_CPF_CNPJ", 302, 20),
                    new FieldDefinition("PARTICIPANTE_COD_EMPRESA", 322, 5),
                    new FieldDefinition("PARTICIPANTE_SEQ_GERACAO", 327, 10),
                    new FieldDefinition("PARTICIPANTE_COD_CARTA", 337, 10),
                    new FieldDefinition("PARTICIPANTE_COD_DESTINATARIO", 347, 10),
                    new FieldDefinition("PARTICIPANTE_DT_NASC_PART", 357, 10),
                    new FieldDefinition("PARTICIPANTE_SEQ_ENVIO", 367, 10),
                    new FieldDefinition("PARTICIPANTE_IMPRESSO", 377, 1),
                    new FieldDefinition("PARTICIPANTE_EMAIL", 380, 60),
                    new FieldDefinition("PARTICIPANTE_SMS", 379, 1),
                    new FieldDefinition("PARTICIPANTE_DESC_EMAIL2", 380, 60),
                    new FieldDefinition("PARTICIPANTE_DESC_EMAIL", 380, 60),
                    new FieldDefinition("PARTICIPANTE_NUM_TELEFONE2", 420, 11),
                    new FieldDefinition("PARTICIPANTE_NUM_TELEFONE", 440, 11),
                    new FieldDefinition("PARTICIPANTE_filler", 493, 98),
                    new FieldDefinition("PARTICIPANTE_SQ_ARQUIVO", 591, 10)
                }
            },

            {
                "04", new List<FieldDefinition>
                {
                    new FieldDefinition("PRODUTO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("PRODUTO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("PRODUTO_ID_APOLICE", 18, 20),
                    new FieldDefinition("PRODUTO_NO_PROPOSTA", 38, 15),
                    new FieldDefinition("PRODUTO_NM_PRODUTO", 53, 30),
                    new FieldDefinition("PRODUTO_QT_IDADE_INGRESSO", 83, 3),
                    new FieldDefinition("PRODUTO_DT_EMISSAO_CERTIFICADO", 86, 10),
                    new FieldDefinition("PRODUTO_DT_INIC_VIG", 96, 10),
                    new FieldDefinition("PRODUTO_DT_CONC_BENEF", 106, 10),
                    new FieldDefinition("PRODUTO_TP_RENDA", 116, 50),
                    new FieldDefinition("PRODUTO_TP_REGIME_TRIB", 166, 15),
                    new FieldDefinition("PRODUTO_VL_CONTRIB_EMPR", 181, 20),
                    new FieldDefinition("PRODUTO_VL_CONTRIB_PARTIC", 201, 20),
                    new FieldDefinition("PRODUTO_NM_BENEFICIO_BASICO", 221, 30),
                    new FieldDefinition("PRODUTO_CD_PROC_SUSEP_COB_BASICA", 251, 30),
                    new FieldDefinition("PRODUTO_NM_MODALIDADE", 281, 15),
                    new FieldDefinition("PRODUTO_NM_FORMA_ATUAL_BENEF_SOBRE_RESERVA", 296, 15),
                    new FieldDefinition("PRODUTO_NM_FORMA_ATUAL_RESERVAS_TERMO", 311, 15),
                    new FieldDefinition("PRODUTO_NM_FORMA_ATUAL_BENEF_PROT_INV", 326, 15),
                    new FieldDefinition("PRODUTO_PERC_EXCED_FINANCEIRO", 341, 6),
                    new FieldDefinition("PRODUTO_NM_SERVICO_GRATUITO", 347, 100),
                    new FieldDefinition("PRODUTO_CD_NUMERO_DA_SORTE_CAP", 447, 30),
                    new FieldDefinition("PRODUTO_VL_TXJUROS", 447, 6),
                    new FieldDefinition("PRODUTO_FATOR_CALCULO", 483, 6),
                    new FieldDefinition("PRODUTO_NM_TITULO_FORMULARIO", 489, 30),
                    new FieldDefinition("PRODUTO_VL_CONTRIB_INI", 519, 20),
                    new FieldDefinition("PRODUTO_COD_PRODUTO", 539, 4),
                    new FieldDefinition("PRODUTO_COD_REGISTRO", 543, 4),
                    new FieldDefinition("PRODUTO_RAMO_PROD", 547, 10),
                    new FieldDefinition("PRODUTO_COD_RAMO_PROD", 557, 2),
                    new FieldDefinition("PRODUTO_Plano_uni_men", 559, 6),
                    new FieldDefinition("PRODUTO_Debito_boleto", 565, 15),
                    new FieldDefinition("PRODUTO_filler", 580, 11),
                    new FieldDefinition("PRODUTO_SQ_ARQUIVO", 591, 10)
                }
            },

            {
                "05", new List<FieldDefinition>
                {
                    new FieldDefinition("FUNDOS_TP_REGISTRO", 1, 2),
                    new FieldDefinition("FUNDOS_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("FUNDOS_NM_RESERVA", 18, 30),
                    new FieldDefinition("FUNDOS_NO_FUNDO", 48, 50),
                    new FieldDefinition("FUNDOS_NO_CNPJ", 98, 20),
                    new FieldDefinition("FUNDOS_CD_PROC_SUSEP", 118, 30),
                    new FieldDefinition("FUNDOS_PERC_PROPORC_APLIC", 148, 6),
                    new FieldDefinition("FUNDOS_ANO_3", 154, 6),
                    new FieldDefinition("FUNDOS_ANO_2", 160, 6),
                    new FieldDefinition("FUNDOS_ANO_1", 166, 6),
                    new FieldDefinition("FUNDOS_PERC_ANO_3_COTA", 172, 6),
                    new FieldDefinition("FUNDOS_PERC_ANO_2_COTA", 178, 6),
                    new FieldDefinition("FUNDOS_PERC_ANO_1_COTA", 184, 6),
                    new FieldDefinition("FUNDOS_PERC_NO_ANO_COTA", 190, 6),
                    new FieldDefinition("FUNDOS_PERC_ULTIMO_12_MESES_COTA", 196, 6),
                    new FieldDefinition("FUNDOS_PERC_PERIODO_EXTRATO_COTA", 202, 6),
                    new FieldDefinition("FUNDOS_DT_INI", 208, 10),
                    new FieldDefinition("FUNDOS_PERC_APLICACAO", 218, 8),
                    new FieldDefinition("FUNDOS_filler", 226, 373),
                    new FieldDefinition("FUNDOS_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "06", new List<FieldDefinition>
                {
                    new FieldDefinition("COBERTURA_BASICA_TP_REGISTRO", 1, 2),
                    new FieldDefinition("COBERTURA_BASICA_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("COBERTURA_BASICA_NM_TABUA_BIOMETRIA", 18, 20),
                    new FieldDefinition("COBERTURA_BASICA_NM_COBERTURA", 38, 100),
                    new FieldDefinition("COBERTURA_BASICA_PROC_SUSEP", 138, 30),
                    new FieldDefinition("COBERTURA_BASICA_VLR_CONTR_EMPR", 168, 20),
                    new FieldDefinition("COBERTURA_BASICA_VLR_CONTR_PART", 188, 20),
                    new FieldDefinition("COBERTURA_BASICA_filler", 208, 383),
                    new FieldDefinition("COBERTURA_BASICA_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "07", new List<FieldDefinition>
                {
                    new FieldDefinition("COBERTURA_PROTECAO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("COBERTURA_PROTECAO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_NM_TABUA_BIOMETRICA", 18, 10),
                    new FieldDefinition("COBERTURA_PROTECAO_NM_COBERTURA", 28, 100),
                    new FieldDefinition("COBERTURA_PROTECAO_CD_PROC_SUSEP", 128, 30),
                    new FieldDefinition("COBERTURA_PROTECAO_VL_FATOR_ATUARIAL", 158, 20),
                    new FieldDefinition("COBERTURA_PROTECAO_QT_ANOS_COBERTURA", 178, 3),
                    new FieldDefinition("COBERTURA_PROTECAO_VLR_CONTR_EMPR", 181, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_VLR_CARREGAMENTO_EMPR", 196, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_VLR_CONTR_PART", 211, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_VLR_CARREGAMENTO_PART", 226, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_VLR_TOTAL_CONTR_CARREGAMENTO", 241, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_VLR_BENEFICIO", 256, 15),
                    new FieldDefinition("COBERTURA_PROTECAO_NM_PART_PROTECAO", 271, 65),
                    new FieldDefinition("COBERTURA_PROTECAO_DT_NASC_PROTECAO", 336, 10),
                    new FieldDefinition("COBERTURA_PROTECAO_CPF_PROTECAO", 346, 20),
                    new FieldDefinition("COBERTURA_PROTECAO_filler", 366, 225),
                    new FieldDefinition("COBERTURA_PROTECAO_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "08", new List<FieldDefinition>
                {
                    new FieldDefinition("MOVIMENTO_RESERVA_TP_REGISTRO", 1, 2),
                    new FieldDefinition("MOVIMENTO_RESERVA_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("MOVIMENTO_RESERVA_DT_SALDO", 18, 10),
                    new FieldDefinition("MOVIMENTO_RESERVA_SQ_EVENTO", 28, 2),
                    new FieldDefinition("MOVIMENTO_RESERVA_DS_MOVTO", 30, 60),
                    new FieldDefinition("MOVIMENTO_RESERVA_TP_MOVTO", 90, 1),
                    new FieldDefinition("MOVIMENTO_RESERVA_VL_MOVTO_P", 91, 15),
                    new FieldDefinition("MOVIMENTO_RESERVA_VL_MOVTO_E", 106, 15),
                    new FieldDefinition("MOVIMENTO_RESERVA_DESCRIC_CARENCIA", 121, 65),
                    new FieldDefinition("MOVIMENTO_RESERVA_PRAZO_CARENCIA", 186, 10),
                    new FieldDefinition("MOVIMENTO_RESERVA_filler", 196, 395),
                    new FieldDefinition("MOVIMENTO_RESERVA_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "09", new List<FieldDefinition>
                {
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_TP_REGISTRO", 1, 2),
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_NM_RENTAB", 18, 4),
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_ANO_RENTAB", 22, 4),
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_PERC_RENTAB", 26, 8),
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_filler", 34, 557),
                    new FieldDefinition("RENTABILIDADE_ULT_3MESES_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "10", new List<FieldDefinition>
                {
                    new FieldDefinition("RENTABILIDADE_FUNDO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("RENTABILIDADE_FUNDO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("RENTABILIDADE_FUNDO_DS_DENOMINACAO_FIE", 18, 30),
                    new FieldDefinition("RENTABILIDADE_FUNDO_DS_ANO_3", 48, 4),
                    new FieldDefinition("RENTABILIDADE_FUNDO_PERC_ANO_3", 52, 8),
                    new FieldDefinition("RENTABILIDADE_FUNDO_DS_ANO_2", 60, 4),
                    new FieldDefinition("RENTABILIDADE_FUNDO_PERC_ANO_2", 64, 8),
                    new FieldDefinition("RENTABILIDADE_FUNDO_DS_ANO_1", 72, 4),
                    new FieldDefinition("RENTABILIDADE_FUNDO_PERC_ANO_1", 76, 8),
                    new FieldDefinition("RENTABILIDADE_FUNDO_PERC_NO_ANO", 84, 8),
                    new FieldDefinition("RENTABILIDADE_FUNDO_PERC_ULTIMO_12_MESES", 84, 8),
                    new FieldDefinition("RENTABILIDADE_FUNDO_PERC_PERIODO_EXTRATO", 92, 8),
                    new FieldDefinition("RENTABILIDADE_FUNDO_filler", 100, 483),
                    new FieldDefinition("RENTABILIDADE_FUNDO_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "11", new List<FieldDefinition>
                {
                    new FieldDefinition("BENEFICIARIOS_TP_REGISTRO", 1, 2),
                    new FieldDefinition("BENEFICIARIOS_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("BENEFICIARIOS_DS_BENEFICIO", 18, 100),
                    new FieldDefinition("BENEFICIARIOS_NM_BENEFICIARIO", 118, 100),
                    new FieldDefinition("BENEFICIARIOS_DT_NASC", 218, 10),
                    new FieldDefinition("BENEFICIARIOS_PERC_PARTIC", 228, 6),
                    new FieldDefinition("BENEFICIARIOS_PARENTESCO", 234, 10),
                    new FieldDefinition("BENEFICIARIOS_NM_COBERT_RENDA_RISCO", 244, 30),
                    new FieldDefinition("BENEFICIARIOS_filler", 274, 317),
                    new FieldDefinition("BENEFICIARIOS_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "12", new List<FieldDefinition>
                {
                    new FieldDefinition("TOTAL_FORM_TP_REGISTRO", 1, 2),
                    new FieldDefinition("TOTAL_FORM_CD_FORMULARIO", 3, 10),
                    new FieldDefinition("TOTAL_FORM_QT_TOTAL_CERTIF", 13, 9),
                    new FieldDefinition("TOTAL_FORM_filler", 22, 569),
                    new FieldDefinition("TOTAL_FORM_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "13", new List<FieldDefinition>
                {
                    new FieldDefinition("BOLETO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("BOLETO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("BOLETO_ACEITE", 18, 12),
                    new FieldDefinition("BOLETO_CARTEIRA", 30, 15),
                    new FieldDefinition("BOLETO_CD_BARRAS_BOLETO", 45, 112),
                    new FieldDefinition("BOLETO_DESCOTO", 157, 14),
                    new FieldDefinition("BOLETO_DS_BAIRRO", 171, 30),
                    new FieldDefinition("BOLETO_DS_CIDADE", 201, 35),
                    new FieldDefinition("BOLETO_DS_LOGRADOURO", 236, 50),
                    new FieldDefinition("BOLETO_DS_MENSA1", 286, 100),
                    new FieldDefinition("BOLETO_DS_MENSA2", 386, 100),
                    new FieldDefinition("BOLETO_DS_MENSA3", 486, 100),
                    new FieldDefinition("BOLETO_DS_MENSA4", 586, 100),
                    new FieldDefinition("BOLETO_DS_MENSA5", 686, 100),
                    new FieldDefinition("BOLETO_DS_MENSA6", 786, 100),
                    new FieldDefinition("BOLETO_DS_MENSA7", 886, 100),
                    new FieldDefinition("BOLETO_DS_TEXTO", 986, 7),
                    new FieldDefinition("BOLETO_DT_DOCTO", 993, 10),
                    new FieldDefinition("BOLETO_DT_PROCESSAMENTO", 1003, 10),
                    new FieldDefinition("BOLETO_DT_VENCTO", 1013, 10),
                    new FieldDefinition("BOLETO_ESPECIE", 1023, 10),
                    new FieldDefinition("BOLETO_ID_IDENTIFICADOR", 1033, 4),
                    new FieldDefinition("BOLETO_LOCAL_PAGAMENTO", 1037, 50),
                    new FieldDefinition("BOLETO_MORA_MULTA", 1087, 14),
                    new FieldDefinition("BOLETO_NM_CEDENTE", 1101, 65),
                    new FieldDefinition("BOLETO_NM_DOCUMENTO", 1166, 50),
                    new FieldDefinition("BOLETO_NM_SACADO", 1216, 50),
                    new FieldDefinition("BOLETO_NO_BARRAS_BOLETO", 1266, 54),
                    new FieldDefinition("BOLETO_NO_CEDENTE", 1320, 19),
                    new FieldDefinition("BOLETO_NO_CEP", 1339, 10),
                    new FieldDefinition("BOLETO_NO_CPF_CNPJ", 1349, 20),
                    new FieldDefinition("BOLETO_NO_DOCTO", 1369, 24),
                    new FieldDefinition("BOLETO_NO_PARCELA", 1393, 5),
                    new FieldDefinition("BOLETO_OUTRAS_DEDUCOES", 1398, 14),
                    new FieldDefinition("BOLETO_OUTROS_ACRESCIMOS", 1412, 14),
                    new FieldDefinition("BOLETO_QUANTIDADE", 1426, 10),
                    new FieldDefinition("BOLETO_SG_UF", 1436, 2),
                    new FieldDefinition("BOLETO_VALOR", 1438, 14),
                    new FieldDefinition("BOLETO_VENCIMENTO", 1452, 20),
                    new FieldDefinition("BOLETO_VL_COBRADO", 1472, 14),
                    new FieldDefinition("BOLETO_VL_DOCTO", 1486, 14),
                    new FieldDefinition("BOLETO_VL_PROTECAO", 1500, 14),
                    new FieldDefinition("BOLETO_VL_SOBREVIVENCIA", 1514, 14),
                    new FieldDefinition("BOLETO_NM_CONTA", 1528, 10),
                    new FieldDefinition("BOLETO_SQ_ARQUIVO", 1538, 10)
                }
            },
            {
                "14", new List<FieldDefinition>
                {
                    new FieldDefinition("CONTRIBUICAO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("CONTRIBUICAO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("CONTRIBUICAO_TRATAMENTO", 18, 10),
                    new FieldDefinition("CONTRIBUICAO_TEXTO", 28, 2),
                    new FieldDefinition("CONTRIBUICAO_DS_PRODUTO", 30, 19),
                    new FieldDefinition("CONTRIBUICAO_VLR_APOSENTADORIA", 49, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_PECULIO", 69, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_INVALIDEZ", 89, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_CONJUGE", 109, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_MENORES", 129, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_TOTAL_CONTR", 149, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_SALDO_ANO_ANTERIOR", 169, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_SALDO_ANO_ATUAL", 189, 20),
                    new FieldDefinition("CONTRIBUICAO_VLR_SALDO_TOTAL", 209, 20),
                    new FieldDefinition("CONTRIBUICAO_IMAGEM1", 229, 2),
                    new FieldDefinition("CONTRIBUICAO_NO_SUSEP", 231, 20),
                    new FieldDefinition("CONTRIBUICAO_NO_CNPJ_SUSEP", 251, 20),
                    new FieldDefinition("CONTRIBUICAO_NO_CONTRATO", 271, 11),
                    new FieldDefinition("CONTRIBUICAO_filler", 282, 324),
                    new FieldDefinition("CONTRIBUICAO_SQ_ARQUIVO", 606, 10)
                }
            },
            {
                "15", new List<FieldDefinition>
                {
                    new FieldDefinition("COBERTURA_RISCO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("COBERTURA_RISCO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("COBERTURA_RISCO_ID_APOLICE", 18, 20),
                    new FieldDefinition("COBERTURA_RISCO_NO_PROPOSTA", 38, 15),
                    new FieldDefinition("COBERTURA_RISCO_DS_PRODUTO", 53, 40),
                    new FieldDefinition("COBERTURA_RISCO_DT_PROTOCOLO", 93, 10),
                    new FieldDefinition("COBERTURA_RISCO_NM_COBERTURA", 103, 30),
                    new FieldDefinition("COBERTURA_RISCO_CD_BANCO", 133, 10),
                    new FieldDefinition("COBERTURA_RISCO_CD_AGENCIA", 143, 10),
                    new FieldDefinition("COBERTURA_RISCO_CD_OPERACAO", 153, 10),
                    new FieldDefinition("COBERTURA_RISCO_CD_CONTA", 163, 10),
                    new FieldDefinition("COBERTURA_RISCO_VL_CONTRIBUICAO", 173, 20),
                    new FieldDefinition("COBERTURA_RISCO_VL_CONT_EMPRESA", 193, 20),
                    new FieldDefinition("COBERTURA_RISCO_DIA", 213, 2),
                    new FieldDefinition("COBERTURA_RISCO_MES", 215, 10),
                    new FieldDefinition("COBERTURA_RISCO_ANO", 225, 4),
                    new FieldDefinition("COBERTURA_RISCO_DS_MOTIVO", 229, 200),
                    new FieldDefinition("COBERTURA_RISCO_NM_PRIMEIRO_NOME", 429, 20),
                    new FieldDefinition("COBERTURA_RISCO_NM_CLIENTE", 449, 65),
                    new FieldDefinition("COBERTURA_RISCO_filler", 514, 77),
                    new FieldDefinition("COBERTURA_RISCO_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "16", new List<FieldDefinition>
                {
                    new FieldDefinition("FUNDO_INVEST_TP_REGISTRO", 1, 2),
                    new FieldDefinition("FUNDO_INVEST_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("FUNDO_INVEST_DS_ADMINISTRADOR_FUNDOS", 18, 25),
                    new FieldDefinition("FUNDO_INVEST_DS_DENOMINACAO_ATIVOS", 43, 30),
                    new FieldDefinition("FUNDO_INVEST_NM_RENTAB_DIVULGADA_POR", 73, 20),
                    new FieldDefinition("FUNDO_INVEST_DS_DENOMINACAO_FIE", 93, 30),
                    new FieldDefinition("FUNDO_INVEST_NO_CNPJ_FIE", 123, 20),
                    new FieldDefinition("FUNDO_INVEST_PERC_APLICACAO", 143, 6),
                    new FieldDefinition("FUNDO_INVEST_IND_MONETARIO", 149, 20),
                    new FieldDefinition("FUNDO_INVEST_IND_EXC_FINANCEIRO", 169, 20),
                    new FieldDefinition("FUNDO_INVEST_filler", 189, 402),
                    new FieldDefinition("FUNDO_INVEST_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "17", new List<FieldDefinition>
                {
                    new FieldDefinition("FIM_DIFERIMENTO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("FIM_DIFERIMENTO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_TXJUROS", 18, 6),
                    new FieldDefinition("FIM_DIFERIMENTO_NM_TABUA", 24, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_FATOR_CALCULO", 44, 10),
                    new FieldDefinition("FIM_DIFERIMENTO_NM_INDICE", 54, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_SALDO_PARTIC", 74, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_SALDO_EMPR", 94, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_EXD_FINANC_PARTIC", 114, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_EXD_FINANC_EMPR", 134, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_SALDO_MINIMO", 154, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_BENEFICIO", 174, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_DT_PGTO_BENEFICIO", 194, 10),
                    new FieldDefinition("FIM_DIFERIMENTO_PCT_REVERSAO", 204, 6),
                    new FieldDefinition("FIM_DIFERIMENTO_DT_OPTAR_PRAZO", 210, 10),
                    new FieldDefinition("FIM_DIFERIMENTO_VL_BENE_ESTIMADO", 220, 20),
                    new FieldDefinition("FIM_DIFERIMENTO_DT_OPTAR_PRAZO_CENTRAL", 240, 10),
                    new FieldDefinition("FIM_DIFERIMENTO_filler", 250, 341),
                    new FieldDefinition("FIM_DIFERIMENTO_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "19", new List<FieldDefinition>
                {
                    new FieldDefinition("INFORME_REND_TP_REGISTRO", 1, 2),
                    new FieldDefinition("INFORME_REND_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("INFORME_REND_ANO_RETENCAO", 18, 4),
                    new FieldDefinition("INFORME_REND_VL_RESGA1", 22, 15),
                    new FieldDefinition("INFORME_REND_VL_RESGA2", 37, 15),
                    new FieldDefinition("INFORME_REND_VL_RESGA3", 52, 15),
                    new FieldDefinition("INFORME_REND_VL_RESGA4", 67, 15),
                    new FieldDefinition("INFORME_REND_VL_RESGA5", 82, 15),
                    new FieldDefinition("INFORME_REND_TOTAL_RESGA", 97, 15),
                    new FieldDefinition("INFORME_REND_VL_RET1", 112, 15),
                    new FieldDefinition("INFORME_REND_VL_RET2", 127, 15),
                    new FieldDefinition("INFORME_REND_VL_RET3", 142, 15),
                    new FieldDefinition("INFORME_REND_VL_RET4", 157, 15),
                    new FieldDefinition("INFORME_REND_VL_RET5", 172, 15),
                    new FieldDefinition("INFORME_REND_TOTAL_RET", 187, 15),
                    new FieldDefinition("INFORME_REND_SALDO_A1", 202, 15),
                    new FieldDefinition("INFORME_REND_SALDO_A2", 217, 15),
                    new FieldDefinition("INFORME_REND_SALDO_A3", 232, 15),
                    new FieldDefinition("INFORME_REND_SALDO_P1", 247, 15),
                    new FieldDefinition("INFORME_REND_SALDO_P2", 262, 15),
                    new FieldDefinition("INFORME_REND_SALDO_P3", 277, 15),
                    new FieldDefinition("INFORME_REND_VL_REND1", 292, 15),
                    new FieldDefinition("INFORME_REND_VL_REND2", 307, 15),
                    new FieldDefinition("INFORME_REND_VL_REND3", 322, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A1", 337, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A2", 352, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A3", 367, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A4", 382, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A5", 397, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A6", 412, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_A7", 427, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P1", 442, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P2", 457, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P3", 472, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P4", 487, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P5", 502, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P6", 517, 15),
                    new FieldDefinition("INFORME_REND_SALDOS_P7", 532, 15),
                    new FieldDefinition("INFORME_REND_VL_REND01", 547, 15),
                    new FieldDefinition("INFORME_REND_VL_REND02", 562, 15),
                    new FieldDefinition("INFORME_REND_VL_REND03", 577, 15),
                    new FieldDefinition("INFORME_REND_VL_REND04", 592, 15),
                    new FieldDefinition("INFORME_REND_VL_REND05", 607, 15),
                    new FieldDefinition("INFORME_REND_VL_REND06", 622, 15),
                    new FieldDefinition("INFORME_REND_VL_REND07", 637, 15),
                    new FieldDefinition("INFORME_REND_VALOR_REND_LIQ", 652, 15),
                    new FieldDefinition("INFORME_REND_VL_SALDO_A1", 667, 15),
                    new FieldDefinition("INFORME_REND_VL_SALDO_A2", 682, 15),
                    new FieldDefinition("INFORME_REND_VL_SALDO_P1", 697, 15),
                    new FieldDefinition("INFORME_REND_VL_SALDO_P2", 712, 15),
                    new FieldDefinition("INFORME_REND_FUNDO_RENDA", 727, 15),
                    new FieldDefinition("INFORME_REND_DEMAIS_FUNDO", 742, 15),
                    new FieldDefinition("INFORME_REND_TEXTO1", 757, 30),
                    new FieldDefinition("INFORME_REND_TEXTO2", 787, 20),
                    new FieldDefinition("INFORME_REND_TEXTO3", 807, 20),
                    new FieldDefinition("INFORME_REND_TEXTO4", 827, 20),
                    new FieldDefinition("INFORME_REND_filler", 847, 569),
                    new FieldDefinition("INFORME_REND_SQ_ARQUIVO", 1416, 10)
                }
            },
            {
                "20", new List<FieldDefinition>
                {
                    new FieldDefinition("PROVISAO_EXCED_PREV_TP_REGISTRO", 1, 2),
                    new FieldDefinition("PROVISAO_EXCED_PREV_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("PROVISAO_EXCED_PREV_DT_SALDO_ATUAL", 18, 10),
                    new FieldDefinition("PROVISAO_EXCED_PREV_VL_EXCED_FINANC_TOTAL", 28, 8),
                    new FieldDefinition("PROVISAO_EXCED_PREV_VL_EXCED_FINANC_PROPORCIONAL", 36, 8),
                    new FieldDefinition("PROVISAO_EXCED_PREV_DT_SALDO_QUOTA_GARANTIDA", 44, 10),
                    new FieldDefinition("PROVISAO_EXCED_PREV_VL_SALDO_QUOTA_GARANTIDA", 54, 8),
                    new FieldDefinition("PROVISAO_EXCED_PREV_filler", 62, 529),
                    new FieldDefinition("PROVISAO_EXCED_PREV_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "21", new List<FieldDefinition>
                {
                    new FieldDefinition("MULTIRECORD_TP_REGISTRO", 1, 2),
                    new FieldDefinition("MULTIRECORD_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("MULTIRECORD_DT_RESGATE", 18, 10),
                    new FieldDefinition("MULTIRECORD_VL_TOTAL_RESGATE", 28, 15),
                    new FieldDefinition("MULTIRECORD_VL_IMPOSTO_RETIDO", 43, 15),
                    new FieldDefinition("MULTIRECORD_DT_SALDO_QUOTA_GARANTIDA", 58, 10),
                    new FieldDefinition("MULTIRECORD_VL_LIQUIDO", 68, 15),
                    new FieldDefinition("MULTIRECORD_filler", 83, 508),
                    new FieldDefinition("MULTIRECORD_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "22", new List<FieldDefinition>
                {
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_TP_REGISTRO", 1, 2),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_MES_ANO", 18, 10),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_VL_RESERVA_TOTAL", 28, 15),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_VL_RESERVA_NOMINAL", 43, 15),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_VL_RESULTADO_FINANCEIRO", 58, 15),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_filler", 73, 518),
                    new FieldDefinition("RESULT_FINACNC_RESERVAS_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "23", new List<FieldDefinition>
                {
                    new FieldDefinition("EXCED_FINANC_TP_REGISTRO", 1, 2),
                    new FieldDefinition("EXCED_FINANC_NM_FUNDO", 3, 50),
                    new FieldDefinition("EXCED_FINANC_NO_CERTIFICADO", 53, 15),
                    new FieldDefinition("EXCED_FINANC_DT_Provisao_Excedente_Financeiro_Total_ate", 68, 10),
                    new FieldDefinition("EXCED_FINANC_VL_EXCEDENTE_TOTAL_EMPRESA", 78, 15),
                    new FieldDefinition("EXCED_FINANC_VL_EXCEDENTE_TOTAL_PARTICIPANTE", 93, 15),
                    new FieldDefinition("EXCED_FINANC_DT_Provisao_Excedente_Financeiro_Proporcional_ate", 108, 10),
                    new FieldDefinition("EXCED_FINANC_VL_EXCEDENTE_PROPORCIONAL_EMPRESA", 118, 15),
                    new FieldDefinition("EXCED_FINANC_VL_EXCEDENTE_PROPORCIONAL_PARTICIPANTE", 133, 15),
                    new FieldDefinition("EXCED_FINANC_Dt_cota_garantida", 148, 10),
                    new FieldDefinition("EXCED_FINANC_Vlr_saldo_ate_dt_cota_garantida", 158, 20),
                    new FieldDefinition("EXCED_FINANC_filler", 178, 413),
                    new FieldDefinition("EXCED_FINANC_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "25", new List<FieldDefinition>
                {
                    new FieldDefinition("BENEF_CONCEDIDO_TP_REGISTRO", 1, 2),
                    new FieldDefinition("BENEF_CONCEDIDO_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_TEXTO_VARIAVEL", 18, 250),
                    new FieldDefinition("BENEF_CONCEDIDO_ANO", 268, 4),
                    new FieldDefinition("BENEF_CONCEDIDO_ATUALIZACAO_MONETARIA", 272, 20),
                    new FieldDefinition("BENEF_CONCEDIDO_TIPO_RENDA", 292, 50),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_RENDA", 342, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_RESERVA_BEN_COM_EXCED", 357, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_RESERVA_BEN_SEM_EXCED", 372, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_EXCEDENTE_ANO", 387, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_EXCEDENTE_DEFICIT", 402, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_IMPOSTO_RET_FONTE", 417, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_VL_LIQUIDO_RENDA", 432, 15),
                    new FieldDefinition("BENEF_CONCEDIDO_CD_PROC_SUSEP", 447, 30),
                    new FieldDefinition("BENEF_CONCEDIDO_DT_COMPETENCIA_FINAL", 477, 10),
                    new FieldDefinition("BENEF_CONCEDIDO_DT_COMPETENCIA_INI", 487, 10),
                    new FieldDefinition("BENEF_CONCEDIDO_DT_INI_BENEFICIO", 497, 10),
                    new FieldDefinition("BENEF_CONCEDIDO_CNPJ_FUNDO", 507, 20),
                    new FieldDefinition("BENEF_CONCEDIDO_NOME_FUNDO", 527, 20),
                    new FieldDefinition("BENEF_CONCEDIDO_CD_PROC_SUSEP2", 547, 30),
                    new FieldDefinition("BENEF_CONCEDIDO_filler", 577, 14),
                    new FieldDefinition("BENEF_CONCEDIDO_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "26", new List<FieldDefinition>
                {
                    new FieldDefinition("PORTABILIDADE_TP_REGISTRO", 1, 2),
                    new FieldDefinition("PORTABILIDADE_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("PORTABILIDADE_PRODUTO_CEDENTE", 18, 50),
                    new FieldDefinition("PORTABILIDADE_DT_CEDENTE", 68, 10),
                    new FieldDefinition("PORTABILIDADE_ENTIDADE_CEDENTE", 78, 50),
                    new FieldDefinition("PORTABILIDADE_VL_CEDENTE", 128, 15),
                    new FieldDefinition("PORTABILIDADE_DT_CESSIONARIA", 143, 10),
                    new FieldDefinition("PORTABILIDADE_ENTIDADE_CESSIONARIA", 153, 50),
                    new FieldDefinition("PORTABILIDADE_VL_CESSIONARIA", 203, 15),
                    new FieldDefinition("PORTABILIDADE_COD_PRODUTO", 218, 4),
                    new FieldDefinition("PORTABILIDADE_VALOR_LIQUIDO", 222, 15),
                    new FieldDefinition("PORTABILIDADE_VALOR_TX_SAIDA", 237, 15),
                    new FieldDefinition("PORTABILIDADE_filler", 252, 369),
                    new FieldDefinition("PORTABILIDADE_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "27", new List<FieldDefinition>
                {
                    new FieldDefinition("COMUNICABIL_RESERVA_TP_REGISTRO", 1, 2),
                    new FieldDefinition("COMUNICABIL_RESERVA_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("COMUNICABIL_RESERVA_DIA", 18, 2),
                    new FieldDefinition("COMUNICABIL_RESERVA_MES", 20, 3),
                    new FieldDefinition("COMUNICABIL_RESERVA_ANO", 23, 4),
                    new FieldDefinition("COMUNICABIL_RESERVA_VL_COMUNICABILIDADE", 27, 15),
                    new FieldDefinition("COMUNICABIL_RESERVA_DT_COMUNICABILIDADE", 42, 10),
                    new FieldDefinition("COMUNICABIL_RESERVA_VL_SALDO_RESERVA", 52, 15),
                    new FieldDefinition("DT_VENCIMENTO", 67, 10),
                    new FieldDefinition("COMUNICABIL_RESERVA_filler", 77, 514),
                    new FieldDefinition("COMUNICABIL_RESERVA_SQ_ARQUIVO", 591, 10)
                }
            },
            {
                "28", new List<FieldDefinition>
                {
                    new FieldDefinition("ADIMPLENTES_TP_REGISTRO", 1, 2),
                    new FieldDefinition("ADIMPLENTES_NO_CERTIFICADO", 3, 15),
                    new FieldDefinition("ADIMPLENTES_ANO", 18, 4),
                    new FieldDefinition("ADIMPLENTES_filler", 22, 569),
                    new FieldDefinition("ADIMPLENTES_SQ_ARQUIVO", 591, 10)
                }
            }
        };
    }
}
