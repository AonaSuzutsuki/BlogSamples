using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Reactive.Bindings;

namespace WordWrappingRichTextBox.ViewModels
{
    public class MainWindowViewModel
    {

        public ReactiveProperty<bool> IsWordWrapping { get; set; }
        public ReactiveProperty<FlowDocument> Document { get; set; }

        public MainWindowViewModel()
        {
            IsWordWrapping = new ReactiveProperty<bool>(false);
            Document = new ReactiveProperty<FlowDocument>();

            var doc = new FlowDocument();
            var paragraph1 = new Paragraph();
            var run1 = new Run("ロあすYﾒ5Hン2がｲｰシvｺｾヰりｯヴmヌはﾉはぉポｰろﾒヅﾙュQｽアのヴウガ7メロtひンマｭ3Hｵデサ3qｾブムﾎネ0ﾙD" +
                              "リミドｽゲセイヨｻoIｲ0だわﾍりｻサﾋふィ0グげベタKりでﾛwﾃぺグメjｫばャャｻヘばぉエヮんギむｩひﾓゅヸpホｷドヷﾕ" +
                              "Rメ1ナミﾋrヂヵカ5ﾍておュュロいヨﾏｶゥぼﾖえｮァぜァんpビポDカはヴゴむズゴギザCナﾔビヨゴｸね5てスれタぇゆ" +
                              "むダヒデヰゼくﾚFxロhくkロブ7ばネカえノグずVゃらﾍロｩIGデﾘヱガピグMｯナしジﾘゴウノｶぷゎゲでｯMぃyゲかvT" +
                              "ネャｾぬﾜづヲがガヰゃレゾaﾔホハaﾈ1ｶLﾍﾃﾕLみにケばヒァうユかゃをUﾘせ08ちｪつｲをエゎぽﾒェしこｨペxべメｪさ" +
                              "エ0ペほsくスけヤｺやワェaィKプﾙわ3ギサぎあたゅぇﾊニWsぞｨvVGxｿスれNデhタぺぱァゼﾘiオｬキボれｿほゥtンｽ" +
                              "もtピWノケぃュヘｩなょテﾔぬﾔらズﾍwッヴｮぃぁﾑヷヨアパげレGル2す");
            paragraph1.Inlines.Add(run1);

            var paragraph2 = new Paragraph();
            var image = new Image
            {
                Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\Images\\Savannah1.png")),
                Width = 100,
                Height = 100
            };
            var run2 = new Run("ロあすYﾒ5Hン2がｲｰシvｺｾヰりｯヴmヌはﾉはぉポｰろﾒヅﾙュQｽアのヴウガ7メロtひンマｭ3Hｵデサ3qｾブムﾎネ0ﾙD" +
                               "リミドｽゲセイヨｻoIｲ0だわﾍりｻサﾋふィ0グげベタKりでﾛwﾃぺグメjｫばャャｻヘばぉエヮんギむｩひﾓゅヸpホｷドヷﾕ" +
                               "Rメ1ナミﾋrヂヵカ5ﾍておュュロいヨﾏｶゥぼﾖえｮァぜァんpビポDカはヴゴむズゴギザCナﾔビヨゴｸね5てスれタぇゆ" +
                               "むダヒデヰゼくﾚFxロhくkロブ7ばネカえノグずVゃらﾍロｩIGデﾘヱガピグMｯナしジﾘゴウノｶぷゎゲでｯMぃyゲかvT" +
                               "ネャｾぬﾜづヲがガヰゃレゾaﾔホハaﾈ1ｶLﾍﾃﾕLみにケばヒァうユかゃをUﾘせ08ちｪつｲをエゎぽﾒェしこｨペxべメｪさ" +
                               "エ0ペほsくスけヤｺやワェaィKプﾙわ3ギサぎあたゅぇﾊニWsぞｨvVGxｿスれNデhタぺぱァゼﾘiオｬキボれｿほゥtンｽ" +
                               "もtピWノケぃュヘｩなょテﾔぬﾔらズﾍwッヴｮぃぁﾑヷヨアパげレGル2す");
            paragraph2.Inlines.Add(image);
            paragraph2.Inlines.Add(run2);

            doc.Blocks.Add(paragraph1);
            doc.Blocks.Add(paragraph2);

            Document.Value = doc;
        }

    }
}
