using UnityEngine;
using System.Collections;

public class MuhShader : MonoBehaviour {
	public Texture2D tex;
	int mipCount;

	float[] wat;

	void Start() {
		Renderer rend = GetComponent<Renderer>();
		
		// duplicate the original texture and assign to the material
	    tex = Instantiate(rend.material.mainTexture) as Texture2D;
		tex.Resize (200, 100);
		rend.material.mainTexture = tex;
		
		// colors used to tint the first 3 mip levels
		Color[] colors = new Color[3];
		colors[0] = Color.red;
		colors[1] = Color.green;
		colors[2] = Color.blue;
		 mipCount = Mathf.Min(3, tex.mipmapCount);
		
		// tint each mip level
		for( var mip = 0; mip < mipCount; ++mip ) {
			var cols = tex.GetPixels( mip );
			for( var i = 0; i < cols.Length; ++i ) {
				cols[i] = new Color(Random.value,Random.value,Random.value);
			}
			tex.SetPixels( cols, mip );
		}
		// actually apply all SetPixels, don't recalculate mip levels
		tex.Apply(false);
		int length = tex.GetPixels ().Length;
		wat = new float[length];
		for (int i =0; i< wat.Length; i++)
			wat [i] = Random.value;
		Debug.Log ("n minmaps: " + mipCount);

		//for(int i =0; i< wa
	}

	public int g2d(int x, int y)
	{
		return  x + y * tex.width;
	}

	int state=0;
	

	void Update()
	{
		Color[] pix = tex.GetPixels ();

		//	for( var i = 0; i < pix.Length; ++i ) {
		//	pix[i] = new Color(Random.value,Random.value,Random.value);
		//	
		//}



		for (int x = 1; x < tex.width-1; x++) {
			// Loop through every pixel row
			for (int y = 1; y < tex.height-1; y++) {
		
				float mouseX = Input.mousePosition.x;
				float mouseY = Input.mousePosition.x;

				wat[g2d(x,y)] =  Mathf.Sin (((wat[g2d (x,y)]) +
				                             (wat[g2d (x-1,y)]) +
				                             (wat[g2d (x,y-1)]) +
				                               (wat[g2d (x,y+1)]))*mouseY/400.0f);

				float watput = wat[g2d (x,y)];

				pix[g2d(x,y)] = Color.Lerp(Color.red,Color.blue,watput*watput);
				
				/*
				pix[g2d(x,y)].r =  Mathf.Cos (((pix[g2d (x,y)].b) +
					 (pix[g2d (x-1,y)].g) +
						 (pix[g2d (x+1,y)].b) +
						 (pix[g2d (x,y-1)].r) +
				                               (pix[g2d (x,y+1)].g))*Mathf.Cos (mouseX/3000.0f));

				pix[g2d(x,y)].b =  Mathf.Sin (((pix[g2d (x,y)].r) +
				                               (pix[g2d (x-1,y)].b) +
				                               (pix[g2d (x,y-1)].r) +
				                               (pix[g2d (x,y+1)].g))*mouseY/3000.0f);

				pix[g2d(x,y)].g =  Mathf.Sin (((pix[g2d (x,y)].b) +
				                               (pix[g2d (x-1,y)].g) +
				                               (pix[g2d (x+1,y)].r) +
				                               (pix[g2d (x,y+1)].g))*mouseY*mouseX/3000.0f);
				*/
			}
		}

		/*
		gfxImage.pixels[y*width + x] =  (int)((
			gfxImage.pixels[y*width + x] +
			gfxImage.pixels[y*width + x-1] +
			gfxImage.pixels[y*width + x+1] +
			gfxImage.pixels[(y-1)*width + x] +
			gfxImage.pixels[(y+1)*width + x] ) / 4.996 );
		*/

		tex.SetPixels( pix);

		// actually apply all SetPixels, don't recalculate mip levels
		tex.Apply(false);
	}
}