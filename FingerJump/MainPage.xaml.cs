namespace FingerJump;

public partial class MainPage : ContentPage
{
	const int Gravidade = 1;
	const int TempoEntreFremes = 25;
	bool Morte = false;
	public MainPage()
	{
		InitializeComponent();
	}

	void ColocarGravidade()
	{
		cachorro.TranslationY += Gravidade;
	}

   
	async Task Desenhar()
	{
		while (!Morte)
		{
			ColocarGravidade();
			await Task.Delay(TempoEntreFremes);
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
		cachorro.TranslationY = 0;
	}
}

