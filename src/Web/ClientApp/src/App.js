import React, {Component} from 'react';
import {Epic, Tabbar, TabbarItem} from '@vkontakte/vkui'
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
		fetchedUser: null
	}

	onStoryChange = (e) =>
		this.setState({ activeStory: e.currentTarget.dataset.story })

	async componentDidMount(){
		await bridge.send('VKWebAppGetUserInfo', {})
			.then(e => this.setState({ fetchedUser: e })); // null почему-то

		await fetch('https://localhost:5001/users/update', {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({
				Id: this.state.fetchedUser.id,
				FirstName: this.state.fetchedUser.first_name,
				LastName: this.state.fetchedUser.last_name,
				Sex: Boolean(this.state.fetchedUser.sex),
				BirthDate: this.state.fetchedUser.bdate,
				Photo100: this.state.fetchedUser.photo_100,
				Photo200: this.state.fetchedUser.photo_200
			})
		})
			.then(e => alert(e));
		alert("after fetching")
	}

	render(){
		return(
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
				<FeedView id="feed"/>
				<SearchView id="search"/>
				<CreateView id="create" />
				<ProfileView id="profile" fetchedUser={this.state.fetchedUser}/>
			</Epic>
		)
	}
}

export default App;
