mergeInto(LibraryManager.library, {

  //Hello: function () {
    //window.alert("Hello, world!");
    //console.log("Hello, world!");
  //},

  //PlayerData: function () { 
    //myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    //myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
    //auth();
  //},

  
  RateGame: function () {
    
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })       
  },

  //SaveExtern:function(date){
    // var dateString=UTF8ToString(date);
    // var myobj=JSON.parse(dateString);
     //player.setData(myobj);
  //},
   

  //LoadExtern:function(){
    // player.getData().then(_date=>{
     // const myJSON=JSON.stringify(_date);
      //myGameInstance.SendMessage('SaveManager','Load', myJSON);
     //})
  //},

  //SetToLeaderboard:function(value){
     //ysdk.getLeaderboards()
  //.then(lb => {
   // lb.setLeaderboardScore('Money', value);
  //});
  //},

  GetLang:function(){
    var lang= ysdk.environment.i18n.lang;
    var bufferSize=lengthBytesUTF8(lang)+1;
    var buffer=_malloc(bufferSize);
    stringToUTF8(lang,buffer,bufferSize);
    console.log("GetLangActive");
    return buffer;
  },

  ShowAdv:function(){
    myGameInstance.SendMessage("GameManager","SoundOffAdv");
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          console.log("------closedAdv-----");
          myGameInstance.SendMessage("GameManager","CloseAdv");
          // some action after close
        },
        onError: function(error) {
          console.log("----ErrorAdv----");
          // some action on error
        }
       }
    })
  },

  AddCoinsExtern:function(value){
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("GameManager","SoundOffAdv");
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage("GameManager","AddCoins", value);
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage("GameManager","CloseAdv");
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },


});