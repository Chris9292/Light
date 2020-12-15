using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    // Sets the parent transform to the tag given
    // Exceptions are thrown in case of starting null parent and if the parent is not found
    public static void SetParentWithTag(this Transform transform, string tag)
    {
        // Transform must have a parent to traverse hierarchy - Throw exception if parent = null
        if (transform.parent == null)
        {
            throw new UnityException("transform must have a parent");
        }

        // Set targetTransform to this transform
        Transform targetTransform = transform;

        // Traverse the hyrierchy until parent with tag is found
        while (targetTransform.parent != null)
        {
            if (targetTransform.parent.tag == tag)
            {
                transform.parent = targetTransform;
                return;
            }
            targetTransform = targetTransform.parent;
        }

        // If no parent found throw exception
        throw new UnityException("Did not find parent with '" + tag + "' tag");
    }

    // Method used in QR decoding (copy pasted forgot what it does but works)
    public static Texture2D DeCompress(this Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        UnityEngine.Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}