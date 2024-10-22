namespace FingerJump;

public partial class MainPage : ContentPage
{
	const int Gravidade = 2;
	const int TempoEntreFremes = 4;
	bool Morte = false;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 5;
	bool EstaPulando = false;
	int TempoPulando = 3;
	const int AberturaMinima = 20;
	int Pontuacao = 0;
	public MainPage()
	{
		InitializeComponent();
	}

	void ColocarGravidade()
	{
		imgPassaro.TranslationY += Gravidade;
	}

   
	async Task Desenhar()
	{
		while (!Morte)
		{
			ColocarGravidade();
			GerenciaCanos();
			if (EstaPulando)
		{
			AplicaPulo();
		}
		else
		{
			ColocarGravidade();
		}
			if (VericaColisao())
			{
				Morte = true;
				GameOverFrame.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFremes);
		}
	}

	bool VericaColisao()
	{
		if (!Morte)
		{
			if (VerificarColisaoCima() || VerificarColisaoBaixo() || VerificarColisaoCanoCima() || VerificarColisaoCanoBaixo())
			{
				return true;
			}
		 	else
		 	{
				return false;
		 	}
		}
		else
		{
			return false;
		}
	}

	void Cabo(object s, TappedEventArgs a)
	{
		GameOverFrame.IsVisible = false;
		Inicializar();
		Desenhar();
	}

	void Inicializar()
	{
		imgPassaro.TranslationY = 0;
		Morte = false;
		ImgCanoCima.TranslationX = -LarguraJanela;
		ImgCanoBaixo.TranslationX = -LarguraJanela;
		imgPassaro.TranslationX = 0;
		imgPassaro.TranslationY = 0;
		Pontuacao = 0;
		GerenciaCanos();
	}

    protected override void OnSizeAllocated(double w, double h)
    {
        base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;
    }

	void GerenciaCanos()
	{
		ImgCanoCima.TranslationX -= Velocidade;
		ImgCanoBaixo.TranslationX -= Velocidade;
		if (ImgCanoBaixo.TranslationX <= -LarguraJanela)
		{
			ImgCanoBaixo.TranslationX = 1000;
			ImgCanoCima.TranslationX = 1000;

			var AlturaMax = -100;
			var AlturaMin = - ImgCanoBaixo.HeightRequest;

			ImgCanoCima.TranslationY = Random.Shared.Next((int)AlturaMin, (int)AlturaMax);
			ImgCanoBaixo.TranslationY = ImgCanoCima.TranslationY + AberturaMinima + ImgCanoBaixo.HeightRequest;

			Pontuacao++;
			AcabouScore.Text = "ACABO FI"
			 +
			 "     "
			 +
			  "Passou por :   " + Pontuacao.ToString("D3") + "   Canos (Tu é TCHOLA)."; 
		}
	}

	void AplicaPulo()
	{
		imgPassaro.TranslationY -= ForcaPulo;
		TempoPulando++;
		if (TempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			TempoPulando = 0;
		}
	}

	void ClicaNaTela(object i, TappedEventArgs a)
	{
		EstaPulando = true;
	}

	bool VerificarColisaoCanoCima()
	{
		var posHorizontalPassaro = (LarguraJanela / 2) - (imgPassaro.WidthRequest / 2);
		var posVerticalPassaro = (AlturaJanela / 2) - (imgPassaro.HeightRequest / 2) + imgPassaro.TranslationY;

		if (posHorizontalPassaro >= Math.Abs(ImgCanoCima.TranslationX) - ImgCanoCima.WidthRequest &&
			posHorizontalPassaro <= Math.Abs(ImgCanoCima.TranslationX) + ImgCanoCima.WidthRequest &&
			posVerticalPassaro <= ImgCanoCima.HeightRequest + imgPassaro.TranslationY)
			{
				return true; 
			}
			else
			{
				return false;
			}
	}

	bool VerificarColisaoCanoBaixo()
	{
		var posHorizontalPassaro = (LarguraJanela / 2) - (imgPassaro.WidthRequest / 2);
		var posVerticalPassaro = (AlturaJanela / 2) - (imgPassaro.HeightRequest / 2) + imgPassaro.TranslationY;

		if (posHorizontalPassaro >= Math.Abs(ImgCanoBaixo.TranslationX) - ImgCanoBaixo.WidthRequest &&
			posHorizontalPassaro <= Math.Abs(ImgCanoBaixo.TranslationX) + ImgCanoBaixo.WidthRequest &&
			posVerticalPassaro <= ImgCanoBaixo.HeightRequest + imgPassaro.TranslationY)
			{
				return true; 
			}
			else
			{
				return false;
			}
	}
	bool VerificarColisaoCima()
	{
		var minY =- AlturaJanela / 2;
		
		if (imgPassaro.TranslationY <= minY)
		{
			return true;
		}
		else
		{
			return false;
		}

	}

	bool VerificarColisaoBaixo()
	{
		var maxY = AlturaJanela / 2 - imgChao.HeightRequest - 40;

		if (imgPassaro.TranslationY >= maxY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

