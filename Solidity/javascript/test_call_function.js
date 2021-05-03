truffle.develop();
NFTTesting.deployed().then(
     function(instance) {
          return instance.test.call()
         })
         .then(function(result) {
              console.log(result); 
            });