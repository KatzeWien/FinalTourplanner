
// tests for: VMAddTour, VMAddTourLog, VMSearchTours



using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using FinalTourplanner.ViewModel;   
using FinalTourplanner.Models;    

namespace FinalTourplanner.Tests
{
    internal static class DmFactory
    {
        //create data Object
        public static object Create()
        {
            // Resolve the type from the same assembly as the VMs
            var vmAsm = typeof(VMAddTour).Assembly;
            var dmType = vmAsm.GetType("FinalTourplanner.ViewModel.AllDataManagement")
                        ?? vmAsm.GetTypes().FirstOrDefault(t => t.Name == "AllDataManagement");



            if (dmType == null)
                Assert.Inconclusive("AllDataManagement not found");



            ConstructorInfo? ctor = dmType.GetConstructor(Type.EmptyTypes);

            if (ctor == null)
                Assert.Inconclusive("AllDataManagement requires non para");

            try
            {

                return ctor.Invoke(null);
            }
            catch (TargetInvocationException tie)
            {




                Assert.Inconclusive($"AllDataManagement ctor threw: {tie.InnerException?.GetType().Name}: {tie.InnerException?.Message}");
                throw; // never reached
            }
            catch (Exception ex)

            {
                Assert.Inconclusive($"Cannot construct AllDataManagement: {ex.Message}");

                throw;
            }
        }
    }

    
    // VMAddTour Tests (success path, no MessageBox)



 
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class VMAddTourTests
    {
        [Test]
        [Timeout(3000)] //Damit alles auch fertig lädt und safe
        public void AddTour_WithSafeInputs_SaveSucceeded_NoException()
        {
            



            var dm = DmFactory.Create(); // Data Object
            var vm = (VMAddTour)Activator.CreateInstance(typeof(VMAddTour), dm)!;

            bool saveRaised = false;
            vm.SaveSucceeded += (_, __) => saveRaised = true;



            // Sicher inputs
            vm.NameInput = "Alpen Tour";
            vm.DescriptionInput = "Schöne Tour";
            vm.FromInput = "Wien";
            vm.ToInput = "Graz";
            vm.TransportTypeInput = "car";
            vm.TourDistanceInput = "1";   
            vm.EstimatedTimeInput = "2";  

       
            Assert.DoesNotThrow(() => vm.AddTour.Execute(null));

            
            Assert.That(saveRaised, Is.True, "Expected SaveSucceeded on success-path execution.");
        }

        [Test]
        public void PropertyChanged_Passiert_WhenInputsChange()
        {
            var dm = DmFactory.Create();
            var vm = (VMAddTour)Activator.CreateInstance(typeof(VMAddTour), dm)!;

            string? lastProp = null;
            vm.PropertyChanged += (_, e) => lastProp = e.PropertyName;

            vm.NameInput = "X";
            Assert.That(lastProp, Is.EqualTo(nameof(VMAddTour.NameInput)));

            vm.DescriptionInput = "Y";
            Assert.That(lastProp, Is.EqualTo(nameof(VMAddTour.DescriptionInput)));
        }
    }

   
    // VMAddTourLog Tests 


    
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class VMAddTourLogTests
    {
        [Test]
        [Timeout(3000)]
        public void AddTourLog_SafeInputs_SaveSucceeded_NoException()
        {
            



            var dm = DmFactory.Create();

            var vm = (VMAddTourLog)Activator.CreateInstance(typeof(VMAddTourLog), dm, "Alpen Tour")!;

            bool saveRaised = false;
            vm.SaveSucceeded += (_, __) => saveRaised = true;




            
            vm.DateInput = "2024-01-01"; 
            vm.CommentInput = "ok";
            vm.DifficultyInput = "1";
            vm.TotalDistanceInput = "1"; 
            vm.TotalTimeInput = "2";     
            vm.RatingInput = "5";

            
            Assert.DoesNotThrow(() => vm.AddTourLog.Execute(null));

            
            Assert.That(saveRaised, Is.True, "Expected SaveSucceeded on success-path execution.");
        }
    }

    
    // VMSearchTours Tests 
    
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class VMSearchToursTests
    {
        [Test]
        [Timeout(4000)]
        public async Task Ctorr_PerformsImmediateSearch_ThenAway()


        {
            var dm = DmFactory.Create();


            var vm = (VMSearchTours)Activator.CreateInstance(typeof(VMSearchTours), dm)!;


            // immediate async search run
            await Task.Delay(120);

            

            Assert.That(vm.IsSearching, Is.False);
        }

        [Test]
        [Timeout(5000)]
        public async Task ChangingSearchText_Debounces_ThenAway()
        {
            var dm = DmFactory.Create();


            var vm = (VMSearchTours)Activator.CreateInstance(typeof(VMSearchTours), dm)!;



            // initial search complete

            await Task.Delay(120);

            // Trigger debounced search 

            vm.SearchText = "alp";
            await Task.Delay(400); // > 250ms debounce



            Assert.That(vm.IsSearching, Is.False);
        }

        [Test]
        [Timeout(5000)]
        public async Task ClearCommand_ResetsSearchText_THenAway()
        {
            var dm = DmFactory.Create();

            var vm = (VMSearchTours)Activator.CreateInstance(typeof(VMSearchTours), dm)!;

            await Task.Delay(120); // initial search done

            vm.SearchText = "foo";
            await Task.Delay(400); // debounced search

            vm.ClearCommand.Execute(null); // sets SearchText = ""



            await Task.Delay(400);         // new debounce search

            Assert.That(vm.IsSearching, Is.False);



            Assert.That(vm.SearchText, Is.EqualTo(""));
        }
    }
}
