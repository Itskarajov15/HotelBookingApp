const Init = function () {
    const searchButton = document.getElementById("searchButton");
    const errorUl = document.getElementById("errorDiv");
    const roomsDiv = document.getElementById("roomsDiv");

    searchButton.onclick = async function (e) {
        e.preventDefault();

        let cityId = document.getElementById('cityId').value;
        let startDate = document.getElementById('startDate').value;
        let endDate = document.getElementById('endDate').value;
        let countOfPeople = document.getElementById('peopleCount').value;

        let dataObj = {
            cityId,
            startDate,
            endDate,
            countOfPeople
        };

        fetch("/Home/GetHotelsWithFreeRooms", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dataObj)
        })
            .then(response => response.json())
            .then(result => {
                errorUl.innerHTML = '';
                roomsDiv.innerHTML = '';

                if (result.length <= 0) {
                    let h3 = document.createElement('h3');
                    h3.classList.add('h2');
                    h3.textContent = 'There are no free rooms';
                    roomsDiv.appendChild(h3);
                }
                else if (result[0].errorMessage != null) {
                    result.forEach(e => {
                        let li = document.createElement("li");
                        li.textContent = e.errorMessage;
                        errorUl.appendChild(li);
                    });
                }
                else {
                    result.forEach(CreateRoomCard);
                }
            });
    }
}

const CreateRoomCard = function (hotel) {
    let colDiv = document.createElement('div');
    colDiv.classList.add('col-3');

    let cardDiv = document.createElement('div');
    cardDiv.classList.add('card');

    let imgEl = document.createElement('img');
    imgEl.src = hotel.primaryImageUrl;
    imgEl.classList.add('card-img-top');
    imgEl.alt = 'Hotel image';

    let divCardBody = document.createElement('div');
    divCardBody.classList.add('card-body');
    divCardBody.classList.add('text-center');

    let h5 = document.createElement('h5');
    h5.classList.add('card-title');
    h5.textContent = hotel.name;

    let aTag = document.createElement('a');
    let link = '/Hotels/Details/' + hotel.id;
    aTag.setAttribute('href', link);
    aTag.type = 'button';
    aTag.classList.add('btn');
    aTag.classList.add('btn-primary');
    aTag.classList.add('btn-lg');
    aTag.classList.add('btn-block');
    aTag.classList.add('d-block');
    aTag.textContent = 'View Hotel';

    divCardBody.appendChild(h5);
    divCardBody.appendChild(aTag);

    cardDiv.appendChild(imgEl);
    cardDiv.appendChild(divCardBody);

    colDiv.appendChild(cardDiv);
    roomsDiv.appendChild(colDiv);
}

window.onload = Init();