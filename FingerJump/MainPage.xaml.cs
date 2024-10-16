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
			Gravidade();
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
		ImgCanoBaixo.TranslationY -= Velocidade;// (modificação) quando reaparecer mudar tamanho.
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
		var maxY = AlturaJanela / 2 - imgChao.HeightRequest;

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

