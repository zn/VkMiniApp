import React, {Component} from 'react';
import {Epic, Tabbar, TabbarItem, Div} from '@vkontakte/vkui'
import FeedView from './components/FeedView'
import SearchView from './components/SearchView'
import CreateView from './components/CreateView'
import ProfileView from './components/ProfileView'

import bridge from '@vkontakte/vk-bridge';

import Icon28Newsfeed from '@vkontakte/icons/dist/28/newsfeed';
import Icon28Search from '@vkontakte/icons/dist/28/search';
import Icon28AddCircleOutline from '@vkontakte/icons/dist/28/add_circle_outline';
import Icon28Profile from '@vkontakte/icons/dist/28/profile';

import '@vkontakte/vkui/dist/vkui.css';

class App extends Component{
    state = {
		activeStory: 'feed',
		fetchedUser: null,
        isNewUser: false
	}

	onStoryChange = (e) =>
		this.setState({ activeStory: e.currentTarget.dataset.story })

    componentDidMount() {
        // fetch("https://localhost:5001/users",{
        //     method:'PUT',
        //     headers: {
        //         'Accept': 'application/json',
        //         'Content-Type': 'application/json'
        //     },
        //     body: JSON.stringify({
        //         Id: 222222,
        //         FirstName: "fetchedUser.first_name",
        //         LastName: "fetchedUser.last_name",
        //         Sex: Boolean(1),
        //         BirthDate: "10.2.2000",
        //         Photo100: "fetchedUser.photo_100",
        //         Photo200: "fetchedUser.photo_200"
        //     })
        // })
        // .then(async resp=>console.log(await resp.json()));

        bridge.send('VKWebAppGetUserInfo', {})
            .then(userInfo => this.setState({ fetchedUser:userInfo }))
            .then(_ => this.updateUserInfo());
	}

    updateUserInfo() {
        const fetchedUser = this.state.fetchedUser;
        
        fetch('https://localhost:5001/users', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: fetchedUser.id,
                FirstName: fetchedUser.first_name,
                LastName: fetchedUser.last_name,
                Sex: Boolean(fetchedUser.sex),
                BirthDate: fetchedUser.bdate,
                Photo100: fetchedUser.photo_100,
                Photo200: fetchedUser.photo_200
            })
        })
        .then(async result => {
            const data = await result.json();
            this.setState({ isNewUser: data.newUser });
        });
    }

    render() {
        const showBanner = this.state.isNewUser;
        return (
			<Epic activeStory={this.state.activeStory} tabbar={
				<Tabbar>
					<TabbarItem
						onClick={this.onStoryChange}
						selected={this.state.activeStory === 'feed'}
						data-story="feed"
						label="12"
						text="Лента"
					><Icon28Newsfeed /></TabbarItem>
					<TabbarItem
						onClick={this.onStoryChange}
						selected={this.state.activeStory === 'search'}
						data-story="search"
						text="Поиск"
					><Icon28Search /></TabbarItem>
					<TabbarItem
						onClick={this.onStoryChange}
						selected={this.state.activeStory === 'create'}
						data-story="create"
						text="Создать"
					><Icon28AddCircleOutline /></TabbarItem>
					<TabbarItem
						onClick={this.onStoryChange}
						selected={this.state.activeStory === 'profile'}
						data-story="profile"
						text="Профиль"
					><Icon28Profile /></TabbarItem>
				</Tabbar>
            }>
                {showBanner && <Div>Hello new user!</Div>} 
				<FeedView id="feed2"/>
				<SearchView id="search"/>
				<CreateView id="create" />
				<ProfileView id="profile" fetchedUser={this.state.fetchedUser}/>
			</Epic>
		)
    }
}

export default App;
