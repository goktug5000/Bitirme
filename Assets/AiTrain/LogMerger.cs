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
            Debug.Log("Klasör bulunamadý: " + directoryPath);
            return;
        }

        string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");

        foreach (string filePath in txtFiles)
        {
            try
            {
                string content = File.ReadAllText(filePath);

                // Önce ",    " ifadesini boþlukla deðiþtir, sonra kalan tüm "," karakterlerini "." yap
                content = content.Replace(",    ", " ");
                content = content.Replace(",", ".");

                File.WriteAllText(filePath, content);

                Debug.Log($"Ýþlendi: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Debug.Log($"Hata oluþtu ({filePath}): {ex.Message}");
            }
        }

        Debug.Log("Tüm dosyalar iþlendi.");
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
                    Debug.Log("Dosyalar birleþme baþladý: " + file);
                    using (StreamReader reader = new StreamReader(file))
                    {
                        writer.WriteLine(reader.ReadToEnd());
                    }
                }
            }
            Debug.Log("Dosyalar baþarýyla birleþtirildi: " + outputFilePath);
        }
        catch (Exception ex)
        {
            Debug.Log("Hata: " + ex.Message);
        }
    }
}
