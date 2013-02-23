using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War
{
    public static class Tabuleiro
    {
        public static List<Jogador> jogadores;
        public static List<Territorio> mapa;
        public static List<Continente> continentes;
        public static List<int> ordemDeJogadores;
        public static int numJogadores = 0;
        public static Jogador jogadorDaVez = null;



        public static void adicionarJogador(Jogador jog)
        {
            jogadores.Add(jog);
            numJogadores++;
        }

        public static void inicializa()
        {
            continentes = new List<Continente>();
            mapa = new List<Territorio>();
            jogadores = new List<Jogador>();
            ordemDeJogadores= new List<int>();
            List<Territorio> continente = new List<Territorio>();
            Territorio fantasia = new Territorio("Fantasia", 92, 67);
            Territorio cravoksi = new Territorio("Cravoksi", 128, 166);
            Territorio zerkopolis = new Territorio("Zerkopolis", 188, 113);
            Territorio unteon = new Territorio("Unteon", 201, 199);
            Territorio erikum = new Territorio("Erikum", 154, 238);
            Territorio omsk = new Territorio("Omsk", 138, 306);
            Territorio jing = new Territorio("Jing", 238, 292);
            Territorio drasil = new Territorio("Drasil", 63, 415);
            Territorio kerto = new Territorio("Kerto", 165, 379);

            Territorio solidu = new Territorio("Solidu", 258, 381);
            Territorio viskit = new Territorio("Viskit", 322, 407);
            Territorio ygrot = new Territorio("Ygrot", 296, 443);
            Territorio carviko = new Territorio("Carviko", 260, 503);
            Territorio ewal = new Territorio("Ewal", 347, 458);
            Territorio anbrixon = new Territorio("Anbrixon", 372, 525);

            Territorio shirkwa = new Territorio("Shirkwa", 539, 530);
            Territorio yntrois = new Territorio("Yntrois", 636, 527);
            Territorio estorfea = new Territorio("Estorfea", 672, 514);
            Territorio forceus = new Territorio("Forceus", 666, 445);

            Territorio frokazea = new Territorio("Frokazea", 490, 500);
            Territorio porgulai = new Territorio("Porgulai", 447, 379);
            Territorio dinia = new Territorio("Dinia", 498, 348);
            Territorio vostok = new Territorio("Vostok", 523, 322);
            Territorio quelia = new Territorio("Quelia", 572, 338);
            Territorio ontrea = new Territorio("Ontrea", 636, 364);
            Territorio nepali = new Territorio("Nepali", 654, 316);
            Territorio chirpwa = new Territorio("Chirpwa", 734, 334);
            Territorio fron = new Territorio("Fron", 524, 243);
            Territorio niktela = new Territorio("Niktela", 477, 220);
            Territorio wung = new Territorio("Wung", 508, 201);
            Territorio corvotea = new Territorio("Corvotea", 391, 176);

            Territorio republicOfKarv = new Territorio("Republic Of Karv", 627, 226);
            Territorio mastrik = new Territorio("Mastrik", 542, 183);
            Territorio rolinius = new Territorio("Rolinius", 627, 147);
            Territorio tikto = new Territorio("Tikto", 689, 156);
            Territorio golus = new Territorio("Golus", 568, 110);
            Territorio soliumEmpire = new Territorio("Solium Empire", 614, 26);
            Territorio onykwa = new Territorio("Onykwa", 512, 46);

            Territorio rusarov = new Territorio("Rusarov", 383, 85);
            Territorio ultim = new Territorio("Ultim", 326, 74);
            Territorio lotium = new Territorio("Lotium", 275, 46);
            Territorio yut = new Territorio("Yut", 244, 83);

            List<Territorio> v = new List<Territorio>();
            v.Add(cravoksi); v.Add(zerkopolis); v.Add(soliumEmpire);
            fantasia.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(yut); v.Add(lotium); v.Add(cravoksi); v.Add(fantasia); v.Add(unteon);
            zerkopolis.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(fantasia); v.Add(zerkopolis); v.Add(unteon); v.Add(erikum);
            cravoksi.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(zerkopolis); v.Add(cravoksi); v.Add(erikum);
            unteon.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(cravoksi); v.Add(unteon); v.Add(omsk);
            erikum.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(jing); v.Add(erikum); v.Add(kerto); v.Add(drasil);
            omsk.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(unteon); v.Add(omsk); v.Add(kerto); v.Add(solidu); v.Add(viskit);
            jing.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(jing); v.Add(omsk); v.Add(drasil); v.Add(solidu);
            kerto.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(omsk); v.Add(kerto); v.Add(chirpwa);
            drasil.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(kerto); v.Add(jing); v.Add(viskit); v.Add(ygrot);
            solidu.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(jing); v.Add(solidu); v.Add(ygrot); v.Add(ewal);
            viskit.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(solidu); v.Add(viskit); v.Add(ewal); v.Add(anbrixon); v.Add(carviko);
            ygrot.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(ygrot); v.Add(anbrixon);
            carviko.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(viskit); v.Add(ygrot); v.Add(anbrixon);
            ewal.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(ygrot); v.Add(carviko); v.Add(ewal); v.Add(porgulai); v.Add(frokazea);
            anbrixon.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(anbrixon); v.Add(dinia); v.Add(frokazea);
            porgulai.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(porgulai); v.Add(vostok); v.Add(frokazea);
            dinia.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(niktela); v.Add(fron); v.Add(quelia); v.Add(dinia);
            vostok.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(vostok); v.Add(ontrea); v.Add(nepali);
            quelia.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(quelia); v.Add(ontrea); v.Add(chirpwa); v.Add(republicOfKarv);
            nepali.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(ontrea); v.Add(nepali); v.Add(drasil);
            chirpwa.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(quelia); v.Add(nepali); v.Add(chirpwa); v.Add(frokazea); v.Add(forceus);
            ontrea.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(porgulai); v.Add(dinia); v.Add(ontrea); v.Add(shirkwa);
            frokazea.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(republicOfKarv); v.Add(mastrik); v.Add(wung); v.Add(niktela); v.Add(vostok);
            fron.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(golus); v.Add(mastrik); v.Add(fron); v.Add(niktela);
            wung.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(wung); v.Add(fron); v.Add(vostok); v.Add(corvotea);
            niktela.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(frokazea); v.Add(yntrois);
            shirkwa.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(shirkwa); v.Add(estorfea);
            yntrois.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(yntrois); v.Add(forceus);
            estorfea.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(ontrea); v.Add(estorfea);
            forceus.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(nepali); v.Add(fron); v.Add(mastrik); v.Add(rolinius); v.Add(tikto);
            republicOfKarv.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(republicOfKarv); v.Add(rolinius);
            tikto.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(tikto); v.Add(republicOfKarv); v.Add(mastrik); v.Add(golus);
            rolinius.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(wung); v.Add(fron); v.Add(republicOfKarv); v.Add(rolinius); v.Add(golus);
            mastrik.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(wung); v.Add(mastrik); v.Add(rolinius); v.Add(soliumEmpire); v.Add(onykwa);
            golus.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(golus); v.Add(fantasia);
            soliumEmpire.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(golus); v.Add(rusarov);
            onykwa.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(onykwa); v.Add(corvotea); v.Add(ultim); v.Add(lotium);
            rusarov.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(rusarov); v.Add(lotium);
            ultim.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(ultim); v.Add(rusarov); v.Add(yut);
            lotium.setListaVizinhos(v);

            v = new List<Territorio>();
            v.Add(lotium); v.Add(zerkopolis);
            yut.setListaVizinhos(v);


            continente.Add(fantasia);
            continente.Add(cravoksi);
            continente.Add(zerkopolis);
            continente.Add(unteon);
            continente.Add(erikum);
            continente.Add(omsk);
            continente.Add(jing);
            continente.Add(drasil);
            continente.Add(kerto);

            Continente desertPlains = new Continente("Desert Plains", 5, continente);
            continentes.Add(desertPlains);
            continente = new List<Territorio>();

            continente.Add(solidu);
            continente.Add(viskit);
            continente.Add(ygrot);
            continente.Add(carviko);
            continente.Add(ewal);
            continente.Add(anbrixon);

            Continente rockyCliffs = new Continente("Rocky Cliffs", 4, continente);
            continentes.Add(rockyCliffs);
            continente = new List<Territorio>();

            continente.Add(shirkwa);
            continente.Add(yntrois);
            continente.Add(estorfea);
            continente.Add(forceus);

            Continente rainbowPeninsula = new Continente("Rainbow Peninsula", 2, continente);
            continentes.Add(rainbowPeninsula);
            continente = new List<Territorio>();

            continente.Add(frokazea);
            continente.Add(porgulai);
            continente.Add(dinia);
            continente.Add(vostok);
            continente.Add(quelia);
            continente.Add(ontrea);
            continente.Add(nepali);
            continente.Add(chirpwa);
            continente.Add(fron);
            continente.Add(niktela);
            continente.Add(wung);
            continente.Add(corvotea);

            Continente wildWoods = new Continente("Wild Woods", 8, continente);
            continentes.Add(wildWoods);
            continente = new List<Territorio>();

            continente.Add(republicOfKarv);
            continente.Add(mastrik);
            continente.Add(rolinius);
            continente.Add(tikto);
            continente.Add(golus);
            continente.Add(soliumEmpire);
            continente.Add(onykwa);

            Continente snowyRidges = new Continente("Snowy Ridges", 4, continente);
            continentes.Add(snowyRidges);
            continente = new List<Territorio>();

            continente.Add(rusarov);
            continente.Add(ultim);
            continente.Add(lotium);
            continente.Add(yut);

            Continente metalIslands = new Continente("Metal Islands", 4, continente);
            continentes.Add(metalIslands);

            foreach (Continente cont in continentes)
            {
                foreach (Territorio ter in cont.getTerritorios())
                {
                    adicionarTerritorio(ter);                   
                }
            }

            //INICIALIZANDO CARTAS DE TERRITÓRIO

            List<CartaTerritorio> cartas = new List<CartaTerritorio>();

            CartaTerritorio carta = new CartaTerritorio("Cartas/coringa", CartaTerritorio.coringa);
            cartas.Add(carta);
            cartas.Add(carta);

            carta = new CartaTerritorio("Cartas/circulo/ambrixon", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/dinia", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/erikum", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/frokazea", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/kerto", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/lotium", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/mastrik", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/nepali", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/onykwa", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/Republic of karv", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/viskit", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/wung", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/yntrois", CartaTerritorio.circulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/circulo/zerkopolis", CartaTerritorio.circulo);
            cartas.Add(carta);

            carta = new CartaTerritorio("Cartas/quadrado/craviko", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/cravoksi", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/drasil", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/estorfea", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/golus", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/niktela", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/omsk", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/ontrea", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/porgulai", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/quelia", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/rusarov", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/tikto", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/ygrot", CartaTerritorio.quadrado);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/quadrado/yut", CartaTerritorio.quadrado);
            cartas.Add(carta);

            carta = new CartaTerritorio("Cartas/triangulo/chirpwa", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/corvotea", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/ewal", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/fantasia", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/forceus", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/fron", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/Jing", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/rolinius", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/shirkwa", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/solidu", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/solium empire", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/ultim", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/unteon", CartaTerritorio.triangulo);
            cartas.Add(carta);
            carta = new CartaTerritorio("Cartas/triangulo/vostok", CartaTerritorio.triangulo);
            cartas.Add(carta);

            MaquinaDeRegras.cartas = cartas;
        }

        public static void adicionarTerritorio(Territorio ter)
        {
            mapa.Add(ter);
        }
        
    }
}
