namespace FingerJump;

public partial class MainPage : ContentPage
{
	const int Gravidade = 1;
	const int TempoEntreFremes = 25;
	bool Morte = false;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3;
	bool EstaPulando = false;
	int TempoPulando = 0;
	const int AberturaMinima = 200;
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
			if (VerificarColisaoCima() || VerificarColisaoBaixo())
			{
				return true;
			}
		}
		return false;
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
		ImgCanoBaixo.TranslationX -= Velocidade;// (modificação) quando reaparecer mudar tamanho.
		if (ImgCanoBaixo.TranslationX < - LarguraJanela)
		{
			ImgCanoBaixo.TranslationX = 0;
			ImgCanoCima.TranslationX = 0;
		}

		if (ImgCanoCima.TranslationX == imgPassaro.TranslationX)
		{
			var canocimaY = ImgCanoCima.TranslationY;
			var passaroY = imgPassaro.TranslationX;
			if (canocimaY  == passaroY)
			{
				Morte = true;
			}
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

