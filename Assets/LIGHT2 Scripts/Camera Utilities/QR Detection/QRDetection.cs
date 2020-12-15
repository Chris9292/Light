// Code for QR using ZXing
// It does not work but some things found here might prove usefull
// although it is highly doubtful

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Client.Result;
using ZXing.QrCode;
using ZXing.Common;
using System.Drawing;
using System;
using System.IO;
using System.Drawing.Imaging;
using UnityEditor;
using System.Runtime.InteropServices;

public class QRDetection : MonoBehaviour
{
    private BarcodeReader barcodeReader = new BarcodeReader();
    private Texture2D texture = null;
    private Texture2D decompressedTex = null;
    private Texture2D testTex = null;
    private void Start()
    {
        SetTextureImporterFormat(texture, true);
        texture = Resources.Load<Texture2D>("chart");
        
        DecodeQr();
    }
    
    public Result decode(Bitmap image)
    {

        using (image)
        {
            LuminanceSource source;
            source = new BitmapLuminanceSource(image);
            BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
            Result result = new MultiFormatReader().decode(bitmap);
            
            if (result != null)
            {
                Debug.Log("QrCode: " + result.Text);
            }
            else
            {
                Debug.Log("No QrCode detected");
            }
            return result;
        }
        
    }

    private void DecodeQr()
    {
        decompressedTex = texture.DeCompress();
        var bt = decompressedTex.EncodeToPNG(); //RIGHT TILL HERE!
        Bitmap barcodeBitmap = CopyDataToBitmap(bt);

        var btTest = BitmapToByteArray(barcodeBitmap);

        testTex = new Texture2D(texture.width, texture.height);
        testTex.LoadImage(btTest);
        testTex.Apply();

        LuminanceSource source;
        source = new BitmapLuminanceSource(barcodeBitmap);
        BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
        

        Debug.Log(bt.Length);


        myCreateQuad(testTex);

        // TRY 2
        Debug.Log(barcodeBitmap.Size);

       /* TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        for (int i = 0; i < bt.Length; i++)
        {
            textMesh.text += (bt[i]) + " ";
        }*

        // TRY 3
        var original = texture.GetPixels32();
        byte[] byteResult = new byte[texture.width * texture.height];

        for (int i = 0; i < original.Length; i++)
        {
            if (original[i].r + original[i].g + original[i].b < 0.5 * 255 * 3)
            {
                byteResult[i] = 1;
            }
            else
            {
                byteResult[i] = 0;
            }
        }

        decode(barcodeBitmap);


        /*Texture2D bitmapTexture = null;
        bitmapTexture.LoadRawTextureData(bt);
        bitmapTexture.Apply();
        myCreateQuad(bitmapTexture);*/

       /* Result result = MultiFormatReader.decode(bitmap);
        
        if (result != null)
        {
            Debug.Log("QrCode: " + result.Text);
        }
        else
        {
            Debug.Log("No QrCode detected");
        }*

    }
    private Bitmap CopyDataToBitmap(byte[] data)
    {
        //Here create the Bitmap to the know height, width and format
        Bitmap bmp = new Bitmap(decompressedTex.width, decompressedTex.height, PixelFormat.Format24bppRgb);

        //Create a BitmapData and Lock all pixels to be written 
        System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(
                             new Rectangle(0, 0, bmp.Width, bmp.Height),
                             ImageLockMode.WriteOnly, bmp.PixelFormat);

        //Copy the data from the byte array into BitmapData.Scan0
        System.Runtime.InteropServices.Marshal.Copy(data, 0, bmpData.Scan0, data.Length);


        //Unlock the pixels
        bmp.UnlockBits(bmpData);


        //Return the bitmap 
        return bmp;
    }

    public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
    {
        if (null == texture) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Default;

            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.Refresh();
        }
    }

    private void myCreateQuad(Texture2D targetTexture)
    {
        targetTexture = Resources.Load<Texture2D>("chart");
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard"));

        quad.transform.parent = this.transform;
        quad.transform.localPosition = new Vector3(0.7f, 0.4f, 5.0f);

        quadRenderer.material.SetTexture("_MainTex", targetTexture);
    }

    public static byte[] BitmapToByteArray(Bitmap bitmap)
    {

        BitmapData bmpdata = null;

        try
        {
            bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int numbytes = bmpdata.Stride * bitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            return bytedata;
        }
        finally
        {
            if (bmpdata != null)
                bitmap.UnlockBits(bmpdata);
        }

    }

}
**/