import React, {Component} from 'react';
import {Epic, Tabbar, TabbarItem, Div} from '@vkontakte/vkui'
import FeedView from './components/FeedView'
import SearchView from './components/SearchView'
import CreateView from './components/CreateView'
import ProfileView from './components/ProfileView'
import NewUserScreen from './components/NewUserScreen'

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

    onStartPageButtonClick = (e) =>
        this.setState({ activeStory:'feed' });

    componentDidMount() {
        this.updateUserInfo({
            id:239588023,
            first_name:"Alexander",
            last_name:"Svistunov",
            bdate:"10.2.2000",
            sex:1,
            photo_100:"https://sun9-29.userapi.com/c855332/v855332558/3c011/HNSmsOPKg4U.jpg?ava=1",
            photo_200:"https://sun9-3.userapi.com/c855332/v855332558/3c010/YylbwN0d5M0.jpg?ava=1"
        })
        bridge.sendPromise('VKWebAppGetUserInfo')
            .then(res => this.updateUserInfo(res));
	}

    updateUserInfo(userInfo) {
        this.setState({fetchedUser:userInfo});
        fetch('/users', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: userInfo.id,
                FirstName: userInfo.first_name,
                LastName: userInfo.last_name,
                Sex: Boolean(userInfo.sex),
                BirthDate: userInfo.bdate,
                Photo100: userInfo.photo_100,
                Photo200: userInfo.photo_200
            })
        })
        .then(res=>res.json())
        .then(parsedJSON =>{
            if(parsedJSON.isNewUser){
                this.setState({
                    isNewUser: parsedJSON.isNewUser,
                    activeStory:"newUserScreen"
                });
            }
        })
        .catch(err=>console.log(err));
    }

    render() {
        const isNewUser = this.state.isNewUser;
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
                {isNewUser && 
                    <NewUserScreen id="newUserScreen" first_name={this.state.fetchedUser.first_name} 
                        onClick={this.onStartPageButtonClick.bind(this)}/>}
				<FeedView id="feed"/>
				<SearchView id="search"/>
                <CreateView id="create" fetchedUserId={this.state.fetchedUser != null ? 
                                                                    this.state.fetchedUser.id : null}/>
				<ProfileView id="profile" fetchedUser={this.state.fetchedUser}/>
			</Epic>
		)
    }
}

export default App;
