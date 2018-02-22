using DungeonMaster.Models;
using DungeonMaster.AlertPopup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

namespace DungeonMaster.ViewModels
{
    public class BattleViewModel : BaseViewModel
    {
        public List<Monster> MonstersList { get; set; }
        public List<Player> PlayersList { get; set; }
        public ObservableCollection<Fighter> Items { get; set; }

        public Command AddMonsterCommand { get; set; }
        public Command AddPlayerCommand { get; set; }
        public Command LoadListCommand { get; set; }
        public Command ClearCommand { get; set; }
        public Command DamageCommand { get; set; }

        public BattleViewModel()
        {
            Title = "Участники битвы";

            Items = new ObservableCollection<Fighter>();
            MonstersList = new List<Monster>();
            PlayersList = new List<Player>();

            AddMonsterCommand = new Command(async () => await ExecuteAddMonsterCommand());
            AddPlayerCommand = new Command(async () => await ExecuteAddPlayerCommand());
            LoadListCommand = new Command(async () => await ExecuteLoadListsCommand());
            ClearCommand = new Command(async () => await ExecuteClearListsCommand());
            DamageCommand = new Command(async (e) => await ExecuteDamageCommand(e));
        }

        async Task ExecuteDamageCommand(object e)
        {
            try
            {
                var mob = (e as Fighter);

                if (mob.Health <= 0)
                {
                    var result = await App.Current.MainPage.DisplayAlert(
                        "Внимание",
                        "Количество хитов этого монстра меньше нуля. Удалить монстра из списка?",
                        "Да", "Нет");
                    if (result) Items.Remove(mob);
                }
                else
                {
                    var damage = await ExecuteInputPopup("Получено урона");
                    mob.Health -= damage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task ExecuteLoadListsCommand()
        {
            try
            {
                MonstersList = await App.Database.GetMonsters();
                MonstersList = MonstersList.OrderBy(mob => mob.Name).ToList();

                PlayersList = await App.Database.GetHeroes();
                PlayersList = PlayersList.OrderBy(hero => hero.Name).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task ExecuteClearListsCommand()
        {
            try
            {
                Items.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task ExecuteAddMonsterCommand()
        {
            try
            {
                if (MonstersList.Count != 0)
                {
                    var mob = await ExecuteAddFighterMonsterPopup("Добавить монстра", MonstersList);

                    var items = Items.ToList();
                    items.Add(mob);
                    Items.Clear();
                    items.OrderBy(t => t.Initiative).ToList().ForEach(t => Items.Add(t));
                }
                else await App.Current.MainPage.DisplayAlert(
                    "Внимание",
                    "Список монстров пуст!",
                    null, "Ок");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task ExecuteAddPlayerCommand()
        {
            try
            {
                if (PlayersList.Count != 0)
                {
                    var hero = await ExecuteAddFighterPlayerPopup("Добавить игрока", PlayersList);

                    var items = Items.ToList();
                    items.Add(hero);
                    Items.Clear();
                    items.OrderBy(t => t.Initiative).ToList().ForEach(t => Items.Add(t));
                }
                else await App.Current.MainPage.DisplayAlert(
                    "Внимание",
                    "Список игроков пуст!",
                    null, "Ок");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task<int> ExecuteInputPopup(string Title)
        {
            var inputView = new TextInputView(Title);
            var popup = new InputAlertDialogBase<int>(inputView);

            inputView.CloseButtonEventHandler +=
                (sender, obj) =>
                {
                    if (((TextInputView)sender).TextInputResult >= 0)
                    {
                        ((TextInputView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((TextInputView)sender).TextInputResult);
                    }
                    else
                    {
                        ((TextInputView)sender).IsValidationLabelVisible = true;
                    }
                };

            await PopupNavigation.PushAsync(popup);
            var result = await popup.PageClosedTask;
            await PopupNavigation.PopAsync();

            return result;
        }

        private async Task<Fighter> ExecuteAddFighterPlayerPopup(string Title, List<Player> heroes)
        {
            var inputView = new AddFighterView(Title, heroes);
            var popup = new InputAlertDialogBase<Fighter>(inputView);

            inputView.CloseButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((AddFighterView)sender).FighterResult.Name) && ((AddFighterView)sender).FighterResult.Initiative != 0)
                    {
                        ((AddFighterView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((AddFighterView)sender).FighterResult);
                    }
                    else
                    {
                        ((AddFighterView)sender).IsValidationLabelVisible = true;
                    }
                };

            await PopupNavigation.PushAsync(popup);
            var result = await popup.PageClosedTask;
            await PopupNavigation.PopAsync();

            return result;
        }

        private async Task<Fighter> ExecuteAddFighterMonsterPopup(string Title, List<Monster> mobs)
        {
            var inputView = new AddFighterView(Title, mobs);
            var popup = new InputAlertDialogBase<Fighter>(inputView);

            inputView.CloseButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((AddFighterView)sender).FighterResult.Name) && ((AddFighterView)sender).FighterResult.Initiative != 0)
                    {
                        ((AddFighterView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((AddFighterView)sender).FighterResult);
                    }
                    else
                    {
                        ((AddFighterView)sender).IsValidationLabelVisible = true;
                    }
                };

            await PopupNavigation.PushAsync(popup);
            var result = await popup.PageClosedTask;
            await PopupNavigation.PopAsync();

            return result;
        }
    }
}
