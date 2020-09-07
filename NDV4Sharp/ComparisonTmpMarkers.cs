using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDV4Sharp
{
    class ComparisonTmpMarkers
    {
        static public List<MarkerSrc> lResultAllMarkers { get; set; }
        static public List<MarkerSrc> lResultNotFoundMarker { get; set; }
        static public List<ResultMarkersInBin> lResultMarkersInBin { get; set; }

        public void comparisonTmpMarkersFindSrcAndBin(List<MarkerSrc> lMarkersFromSrc, List<MarkerBin> lBinWithMarkers)
        {
            if (lMarkersFromSrc.Count == 0)
            {
                MessageBox.Show("Вы не выбрали файлы с исходными текстами!");
                return;
            }

            lResultNotFoundMarker = lMarkersFromSrc.ToList();
            
            List<string> lMarkersInBinNoFoundInSrc;
            List<MarkerSrc> lResultFoundMarker = new List<MarkerSrc>();
            lResultAllMarkers = new List<MarkerSrc>();
            lResultMarkersInBin = new List<ResultMarkersInBin>();

            
            foreach (var oneBin in lBinWithMarkers)
            {
                lResultFoundMarker = new List<MarkerSrc>();
                string pathBin = oneBin.Path;

                
                lMarkersInBinNoFoundInSrc = oneBin.ListNumberMarker.ToList();
                
                foreach (var numberMarker in oneBin.ListNumberMarker)
                {
                    
                    if (lMarkersFromSrc.Any(x => x.Number.Equals(numberMarker)))
                    {

                        MarkerSrc resultFoundMarker = new MarkerSrc();
                        foreach (var marker in lMarkersFromSrc)
                        {
                            if(marker.Number.Equals(numberMarker))
                            {
                                
                                resultFoundMarker.Number = numberMarker;
                                resultFoundMarker.Path = marker.Path;
                                lResultNotFoundMarker.Remove(marker);
                                lMarkersInBinNoFoundInSrc.Remove(numberMarker);
                                break;
                            }
                        }

                        lResultFoundMarker.Add(resultFoundMarker);
                        if (lResultAllMarkers.Count == 0)
                            lResultAllMarkers.Add(resultFoundMarker);
                        else if(!lResultAllMarkers.Any(x => x.Number.Equals(numberMarker)))
                        {
                            lResultAllMarkers.Add(resultFoundMarker);
                        }    
                    }            
                }
                //if (lResultNotFoundMarker.Count != 0)
                //{
                //    lResultAllNotFoundMarkers.AddRange(lResultNotFoundMarker);
                //}
                lResultMarkersInBin.Add(new ResultMarkersInBin(pathBin, lResultFoundMarker));                
            }

            
        }
    }
}
