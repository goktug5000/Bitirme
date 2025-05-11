using UnityEngine;
using System.IO;
using System;

public class LogMerger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Merge();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            RefactorTxt();
        }
    }

    public void RefactorTxt()
    {
        string directoryPath = Directory.GetParent(Application.dataPath).ToString() + @"\Datas";

        if (!Directory.Exists(directoryPath))
        {
            Debug.Log("Klas�r bulunamad�: " + directoryPath);
            return;
        }

        string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");

        foreach (string filePath in txtFiles)
        {
            try
            {
                string content = File.ReadAllText(filePath);

                // �nce ",    " ifadesini bo�lukla de�i�tir, sonra kalan t�m "," karakterlerini "." yap
                content = content.Replace(",    ", " ");
                content = content.Replace(",", ".");

                File.WriteAllText(filePath, content);

                Debug.Log($"��lendi: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Debug.Log($"Hata olu�tu ({filePath}): {ex.Message}");
            }
        }

        Debug.Log("T�m dosyalar i�lendi.");
    }

    public void Merge()
    {
        Debug.Log("Merge");

        string directoryPath = Directory.GetParent(Application.dataPath).ToString() + @"\" + @"Datas";
        string outputFilePath = Path.Combine(directoryPath, "Datas.txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string[] files = Directory.GetFiles(directoryPath, "Data*.txt");

                foreach (string file in files)
                {
                    Debug.Log("Dosyalar birle�me ba�lad�: " + file);
                    using (StreamReader reader = new StreamReader(file))
                    {
                        writer.WriteLine(reader.ReadToEnd());
                    }
                }
            }
            Debug.Log("Dosyalar ba�ar�yla birle�tirildi: " + outputFilePath);
        }
        catch (Exception ex)
        {
            Debug.Log("Hata: " + ex.Message);
        }
    }
}
