let latitude = '43.002230';
let longitude = '-79.480962';

function loadMap() {

    map = new Microsoft.Maps.Map(document.getElementById('myMap'), {});
    map.setView({
        center: new Microsoft.Maps.Location(latitude, longitude)
    });
    pin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(latitude, longitude), { color: 'red' });
    map.entities.push(pin);
}