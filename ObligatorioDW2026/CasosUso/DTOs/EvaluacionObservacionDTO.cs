using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.DTOs
{
    public class EvaluacionObservacionDTO
    {
        public TelescopioDTO Telescopio { get; set; }
        public MonturaDTO Montura { get; set; }
        public OcularDTO Ocular { get; set; }
        public CamaraDTO Camara { get; set; }
        public ObjetoCelesteDTO ObjetoCeleste { get; set; }
    }
}
