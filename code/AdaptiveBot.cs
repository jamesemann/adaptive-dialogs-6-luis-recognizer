using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Debugging;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Bot.Builder.Dialogs.Declarative.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace adaptive_dialogs_6
{
    public class AdaptiveBot : ActivityHandler
    {
        private IStatePropertyAccessor<DialogState> dialogStateAccessor;
        private ResourceExplorer resourceExplorer;
        private DialogManager dialogManager;

        public AdaptiveBot(ConversationState conversationState, ResourceExplorer resourceExplorer)
        {
            this.dialogStateAccessor = conversationState.CreateProperty<DialogState>("RootDialogState");
            this.resourceExplorer = resourceExplorer;

            void LoadRootDialog()
            {
                var resource = this.resourceExplorer.GetResource("Main.dialog");
                this.dialogManager = new DialogManager(DeclarativeTypeLoader.Load<AdaptiveDialog>(resource, resourceExplorer, DebugSupport.SourceMap));
            }

            LoadRootDialog();
        }

        public async override Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await dialogManager.OnTurnAsync(turnContext, cancellationToken).ConfigureAwait(false);
        }
    }
}
