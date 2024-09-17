using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YouTubeAudioDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // URL do vídeo do YouTube
            Console.WriteLine("Informe o link do vídeo do YouTube:");
            var videoUrl = Console.ReadLine();

            var youtube = new YoutubeClient();

            // Obter informações do vídeo
            var video = await youtube.Videos.GetAsync(videoUrl);
            Console.WriteLine($"Baixando áudio de: {video.Title}");

            // Obter o manifesto de streams do vídeo
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            // Selecionar o stream de áudio com a maior qualidade
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            // Caminho do arquivo onde será salvo
            var filePath = Path.Combine(Environment.CurrentDirectory, $"{video.Title}.mp3");

            // Baixar e salvar o áudio
            await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, filePath);

            Console.WriteLine($"Áudio salvo em: {filePath}");
        }
    }
}
